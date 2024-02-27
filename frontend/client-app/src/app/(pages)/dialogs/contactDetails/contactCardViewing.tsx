import * as React from 'react';

import Box from '@mui/material/Box';
import Divider from '@mui/material/Divider';
import EditIcon from '@mui/icons-material/Edit';

import SvgIcon from '@mui/material/SvgIcon';
import IconButton from '@mui/material/IconButton';
import PhoneIcon from '@mui/icons-material/Phone';
import EmailIcon from '@mui/icons-material/Email';

import { contactCard, flexRow, styleIcon, styleDivider, flexColumn } from './cardContactStyle';
import { UseStoresForContacts } from '../../contacts/page';
import CostomAvatar from '../../../component/customAvatar';

export default function ContactCardViewing() {
  const { contactDetailsStore } = UseStoresForContacts();

  const PhoneList = () => {
    if (!contactDetailsStore.contact.phoneNumbers) {
      return;
    }
    return (
      <Box sx={flexColumn}>
        {contactDetailsStore.contact.phoneNumbers.map((value, index) => (
          <p key={index}>{value.number}</p>
        ))}
      </Box>
    );
  };

  const MailList = () => {
    if (!contactDetailsStore.contact.mails) {
      return;
    }
    return (
      <Box sx={flexColumn}>
        {contactDetailsStore.contact.mails.map((value, index) => (
          <p key={index}>{value.email}</p>
        ))}
      </Box>
    );
  };

  return (
    <Box sx={contactCard}>
      <CostomAvatar fullName={contactDetailsStore.contact.name} />
      <p>{contactDetailsStore.contact.name}</p>

      <Divider sx={styleDivider} />

      <IconButton onClick={() => contactDetailsStore.onEditMod()}>
        <EditIcon />
      </IconButton>

      <Divider sx={styleDivider} />

      <Box key="phoneNumbers" sx={flexRow}>
        <SvgIcon fontSize="small" component={PhoneIcon} sx={styleIcon} />
        <PhoneList />
      </Box>

      <Divider sx={styleDivider} />

      <Box key="Mails" sx={flexRow}>
        <SvgIcon fontSize="small" component={EmailIcon} sx={styleIcon} />
        <MailList />
      </Box>

      <Divider sx={styleDivider} />
      <p>{contactDetailsStore.contact.description}</p>
    </Box>
  );
}
