'use client';
import * as React from 'react';
import { useEffect } from 'react';

import Container from '@mui/material/Container';

import Button from '@mui/material/Button';

import Pagination from '@mui/material/Pagination';
import SnackbarAlert from '../dialogs/snackbarAlert';

import { observer } from 'mobx-react-lite';
import { ShowcaseStore } from '@/app/stores/showcaseStore';

import TableContacts from './tableContacts';
import SearchPanel from './searchPanel';
import NewContact from '../dialogs/addNewContact';
import ContactDetails from '../dialogs/contactDetails/contactDetails';

export const ShowcaseStoreContext = React.createContext<ShowcaseStore | undefined>(undefined);
export const UseShowcaseStore = (): ShowcaseStore => {
  return React.useContext(ShowcaseStoreContext)!;
};

export default function Contacts() {
  useEffect(() => {
    showcaseStore.contactsStore.getContacts();
    return () => {};
  }, []);

  const showcaseStore = new ShowcaseStore();

  const PaginationContacts = observer(() => {
    const handleChange = (event: React.ChangeEvent<unknown>, value: number) => {
      showcaseStore.contactsStore.setCurrentPage(value);
    };
    return (
      <Pagination
        count={showcaseStore.contactsStore.paginationState.allPage}
        page={showcaseStore.contactsStore.paginationState.currentPage}
        onChange={handleChange}
      />
    );
  });

  return (
    <ShowcaseStoreContext.Provider value={showcaseStore}>
      <Container maxWidth="md">
        <SearchPanel />
        <TableContacts />
        <PaginationContacts />

        <SnackbarAlert />

        <Button onClick={() => showcaseStore.newContactStore.open()} variant="contained">
          Добавить
        </Button>
        <ContactDetails actionAfterClosing={() => showcaseStore.contactsStore.getContacts()} />
        <NewContact actionAfterClosing={() => showcaseStore.contactsStore.getContacts()} />
      </Container>
    </ShowcaseStoreContext.Provider>
  );
}
