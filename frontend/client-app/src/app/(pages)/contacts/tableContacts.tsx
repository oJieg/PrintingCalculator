import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';

import SvgIcon from '@mui/material/SvgIcon';
import PhoneIcon from '@mui/icons-material/Phone';
import EmailIcon from '@mui/icons-material/Email';
import CustomAvatar from '../../component/customAvatar';
import DropDownList from './dropDownList';

import Box from '@mui/material/Box';
import Skeleton from '@mui/material/Skeleton';

import Alert from '@mui/material/Alert';

import { observer } from 'mobx-react-lite';
import { UseShowcaseStore } from './page';

import { activeRow, inactiveRow } from './styleContacts';
export default function TableContacts() {
  const { contactsStore, contactDetailsStore } = UseShowcaseStore();

  const DataForTable = observer(() => {
    return contactsStore.contacts.map((row) => (
      <TableRow
        hover={row.id! >= -1}
        key={row.id}
        sx={() => {
          if (row.id! < 0) {
            return inactiveRow;
          }
          return activeRow;
        }}
        onClick={() => {
          if (row.id! >= 0) {
            contactDetailsStore.open(row.id);
          }
        }}>
        <TableCell sx={{ width: 30 }}>
          <CustomAvatar fullName={row.name} size={30} inactivated={row.id! < 0} />
        </TableCell>
        <TableCell>{row.name}</TableCell>

        <TableCell sx={{ whiteSpace: 'nowrap', minWidth: 150 }}>
          <DropDownList list={row.mails?.map((x) => x.email)} id={row.id + 'd'} />
        </TableCell>
        <TableCell sx={{ whiteSpace: 'nowrap', minWidth: 150 }}>
          <DropDownList list={row.phoneNumbers?.map((x) => x.number)} id={row.id + 'p'} />
        </TableCell>
        <TableCell>{row.description}</TableCell>
      </TableRow>
    ));
  });

  const BodyTable = observer(() => {
    if (contactsStore.failSerser) {
      return <Alert severity="error">Ошибка сервера!</Alert>;
    }

    if (contactsStore.openProgressBar) {
      let skelet: React.JSX.Element[] = [];
      for (let i = 0; i < contactsStore.paginationState.take; i++) {
        skelet[i] = (
          <Skeleton key={i} variant="rectangular" width={'100%'} height={70} sx={{ mb: '5px' }} />
        );
      }
      return (
        <Box>
          <Skeleton variant="rectangular" width={'100%'} height={38.5} sx={{ mb: '5px' }} />
          {skelet.map((value) => value)}
        </Box>
      );
    }

    return (
      <TableContainer>
        <Table size="small" aria-label="a dense table">
          <TableHead>
            <TableRow>
              <TableCell></TableCell>
              <TableCell>Имя</TableCell>
              <TableCell>
                <SvgIcon component={EmailIcon} />
              </TableCell>
              <TableCell>
                <SvgIcon component={PhoneIcon} />
              </TableCell>
              <TableCell>Описание</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            <DataForTable />
          </TableBody>
        </Table>
      </TableContainer>
    );
  });

  return <BodyTable />;
}
