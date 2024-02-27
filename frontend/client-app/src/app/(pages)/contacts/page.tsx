'use client';
import * as React from 'react';
import { useEffect } from 'react';

import Container from '@mui/material/Container';

import Button from '@mui/material/Button';

import Pagination from '@mui/material/Pagination';
import SnackbarAlert from '../dialogs/snackbarAlert';

import { observer } from 'mobx-react-lite';
import { StoresForContacts} from '../../stores/storesForContacts'

import TableContacts from './tableContacts';
import SearchPanel from './searchPanel';
import NewContact from '../dialogs/addNewContact';
import ContactDetails from '../dialogs/contactDetails/contactDetails';

export const StoresForContactsContext = React.createContext<StoresForContacts | undefined>(undefined);
export const UseStoresForContacts = (): StoresForContacts => {
  return React.useContext(StoresForContactsContext)!;
};

export default function Contacts() {
  useEffect(() => {
    storesForContacts.contactsStore.getContacts();
    return () => {};
  }, []);

  const storesForContacts = new StoresForContacts();

  const PaginationContacts = observer(() => {
    const handleChange = (event: React.ChangeEvent<unknown>, value: number) => {
      storesForContacts.contactsStore.setCurrentPage(value);
    };
    return (
      <Pagination
        count={storesForContacts.contactsStore.paginationState.allPage}
        page={storesForContacts.contactsStore.paginationState.currentPage}
        onChange={handleChange}
      />
    );
  });

  return (
    <StoresForContactsContext.Provider value={storesForContacts}>
      <Container maxWidth="md">
        <SearchPanel />
        <TableContacts />
        <PaginationContacts />

        <SnackbarAlert />

        <Button onClick={() => storesForContacts.newContactStore.open()} variant="contained">
          Добавить
        </Button>
        <ContactDetails actionAfterClosing={() => storesForContacts.contactsStore.getContacts()} />
        <NewContact actionAfterClosing={() => storesForContacts.contactsStore.getContacts()} />
      </Container>
    </StoresForContactsContext.Provider>
  );
}
