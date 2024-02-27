import * as React from 'react';

import Dialog from '@mui/material/Dialog';
import Box from '@mui/material/Box';
import Card from '@mui/material/Card';
import Stack from '@mui/material/Stack';

import { observer } from 'mobx-react-lite';
import { UseStoresForContacts } from '../../contacts/page';
import ContactCard from './contactCard';

interface ContactDetailsProps {
  actionAfterClosing?: () => void;
}

export default function ContactDetails(report: ContactDetailsProps) {
  const { contactDetailsStore } = UseStoresForContacts();

  const onCloseDetailContact = () => {
    contactDetailsStore.close();
    if (report.actionAfterClosing) {
      report.actionAfterClosing();
    }
  };
  const DialogContactDetails = observer(() => {
    return (
      <Dialog onClose={() => onCloseDetailContact()} open={contactDetailsStore.isOpen}>
        <Stack direction="row" spacing={2}>
          <Box>
            <ContactCard />
          </Box>
          <Box>
            <Card>Тут будет табличка с расчетами произведенными для этого контакта</Card>
          </Box>
        </Stack>
      </Dialog>
    );
  });

  return <DialogContactDetails />;
}
