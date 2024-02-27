import * as React from 'react';

import Button from '@mui/material/Button';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogContentText from '@mui/material/DialogContentText';

import { UseStoresForContacts } from '../contacts/page';
import { observer } from 'mobx-react-lite';

export default function Confirmation() {
  const { confirmationStore } = UseStoresForContacts();

  const ConfirmationDialog = observer(() => {
    return (
      <Dialog
        open={confirmationStore.isOpen}
        onClose={() => confirmationStore.cancel()}
        aria-labelledby="alert-dialog-title"
        aria-describedby="alert-dialog-description">
        <DialogContent>
          <DialogContentText id="alert-dialog-description">
            {confirmationStore.message}
          </DialogContentText>
        </DialogContent>
        <DialogActions>
          <Button onClick={() => confirmationStore.cancel()} autoFocus>
            Отмена
          </Button>
          <Button key="conf" onClick={() => confirmationStore.confirm()}>
            Принять
          </Button>
        </DialogActions>
      </Dialog>
    );
  });

  return <ConfirmationDialog />;
}
