import * as React from 'react';

import Snackbar from '@mui/material/Snackbar';
import Alert from '@mui/material/Alert';
import { alertStore } from '../../stores/alert-store';
import { UseStoresForContacts } from '../contacts/page';
import { observer } from 'mobx-react-lite';

export default function SnackbarAlert() {
  const AlertCustom = observer(() => {
    return (
      <Snackbar
        open={alertStore.alert.open}
        autoHideDuration={6000}
        onClose={() => alertStore.closeAlert()}>
        <Alert severity={alertStore.alert.severity}>{alertStore.alert.massage}</Alert>
      </Snackbar>
    );
  });

  return <AlertCustom />;
}
