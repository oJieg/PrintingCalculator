import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogContentText from '@mui/material/DialogContentText';
import DialogTitle from '@mui/material/DialogTitle';

import Button from '@mui/material/Button';

import TextField from '@mui/material/TextField';

import SvgIcon from '@mui/material/SvgIcon';
import PhoneIcon from '@mui/icons-material/Phone';
import EmailIcon from '@mui/icons-material/Email';
import IconButton from '@mui/material/IconButton';
import DeleteIcon from '@mui/icons-material/Delete';
import AddIcon from '@mui/icons-material/Add';

import Box from '@mui/material/Box';

import { UseShowcaseStore } from '../contacts/page';
import { observer } from 'mobx-react-lite';
import Confirmation from './confirmation';

import { styleRow, styleIconMailAndPhone } from './styleDialogs';

interface NewContactProps {
  actionAfterClosing?: () => void;
}
export default function NewContact(report: NewContactProps) {
  const { newContactStore, confirmationStore } = UseShowcaseStore();
  const MailsField = observer(() => {
    return (
      <Box key="mails">
        {newContactStore.mailsContact.map((value, index) => {
          return (
            <Box key={'mail' + index} sx={styleRow}>
              <TextField
                margin="dense"
                key={'mail' + index.toString()}
                label="Описание"
                type="text"
                fullWidth
                variant="standard"
                value={value.email}
                onChange={(event: React.ChangeEvent<HTMLInputElement>) => {
                  newContactStore.setMailContact(index, event.target.value);
                }}
              />
              <IconButton
                key={'buttonDelNumber' + index}
                onClick={() =>
                  confirmationStore.open(
                    () => newContactStore.deleteMail(index),
                    'Удалить email -  ' + value.email + '?'
                  )
                }>
                <DeleteIcon />
              </IconButton>
            </Box>
          );
        })}
      </Box>
    );
  });

  const PhoneNumberField = observer(() => {
    return (
      <Box key="phones">
        {newContactStore.phoneNumbersContact.map((value, index) => {
          return (
            <Box key={'phone' + index} sx={styleRow}>
              <TextField
                margin="dense"
                key={'phoneNumber' + index.toString()}
                label="Описание"
                type="text"
                fullWidth
                variant="standard"
                value={value.number}
                onChange={(event: React.ChangeEvent<HTMLInputElement>) => {
                  newContactStore.setPhoneNumber(index, event.target.value);
                }}
              />
              <IconButton
                key={'buttonDelNumber' + index}
                onClick={() =>
                  confirmationStore.open(
                    () => newContactStore.deletePhoneNumber(index),
                    'Удалить телефон - ' + value.number + '?'
                  )
                }>
                <DeleteIcon />
              </IconButton>
            </Box>
          );
        })}
      </Box>
    );
  });

  const ContentDialog = observer(() => {
    return (
      <DialogContent>
        <DialogContentText>Создание нового контакта</DialogContentText>
        <TextField
          autoFocus
          margin="dense"
          id="name"
          label="Имя"
          type="text"
          fullWidth
          variant="standard"
          value={newContactStore.nameContact}
          onChange={(event: React.ChangeEvent<HTMLInputElement>) => {
            newContactStore.setNameContact(event.target.value);
          }}
        />
        <Box sx={styleRow}>
          <SvgIcon fontSize="small" component={EmailIcon} sx={styleIconMailAndPhone} />
          <MailsField />
          <IconButton onClick={() => newContactStore.addMail()}>
            <AddIcon />
          </IconButton>
        </Box>

        <Box sx={styleRow}>
          <SvgIcon fontSize="small" component={PhoneIcon} sx={styleIconMailAndPhone} />
          <PhoneNumberField />
          <IconButton onClick={() => newContactStore.addPhoneNumber()}>
            <AddIcon />
          </IconButton>
        </Box>

        <TextField
          margin="dense"
          id="description"
          label="Описание"
          type="text"
          fullWidth
          variant="standard"
          value={newContactStore.descriptionContact}
          onChange={(event: React.ChangeEvent<HTMLInputElement>) => {
            newContactStore.setDescriptionContact(event.target.value);
          }}
        />
      </DialogContent>
    );
  });
  const actionAfterClosing = () => {
    if (report.actionAfterClosing) {
      report.actionAfterClosing();
    }
  };
  const DialogNewContact = observer(() => {
    return (
      <Dialog
        onClose={() => {
          newContactStore.close();
        }}
        open={newContactStore.isOpen}>
        <DialogTitle>Добавить</DialogTitle>
        <ContentDialog />
        <DialogActions>
          <Button
            onClick={() => {
              newContactStore.close();
            }}>
            Cancel
          </Button>
          <Button
            onClick={() => {
              newContactStore.saveData();
              actionAfterClosing();
            }}>
            Subscribe
          </Button>
        </DialogActions>
      </Dialog>
    );
  });

  return (
    <Box>
      <DialogNewContact />
      <Confirmation />
    </Box>
  );
}
