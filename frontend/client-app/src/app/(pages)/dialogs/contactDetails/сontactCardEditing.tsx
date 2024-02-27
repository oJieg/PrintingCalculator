import * as React from 'react';

import Box from '@mui/material/Box';
import TextField from '@mui/material/TextField';
import CostomAvatar from '../../../component/customAvatar';
import Divider from '@mui/material/Divider';
import EditIcon from '@mui/icons-material/Edit';
import AddIcon from '@mui/icons-material/Add';

import IconButton from '@mui/material/IconButton';
import SvgIcon from '@mui/material/SvgIcon';
import PhoneIcon from '@mui/icons-material/Phone';
import EmailIcon from '@mui/icons-material/Email';
import SaveIcon from '@mui/icons-material/Save';
import CancelIcon from '@mui/icons-material/Cancel';
import DeleteIcon from '@mui/icons-material/Delete';

import { contactCard, flexRow, styleIcon, styleDivider } from './cardContactStyle';

import { observer } from 'mobx-react-lite';
import { UseStoresForContacts } from '../../contacts/page';

export default function ContactCardEditing() {
  const { contactDetailsStore, confirmationStore } = UseStoresForContacts();

  const AvatarCostom = observer(() => {
    return <CostomAvatar fullName={contactDetailsStore.contact.name} />;
  });

  const NameTextField = observer(() => {
    return (
      <TextField
        margin="dense"
        id="inputName"
        type="text"
        fullWidth
        variant="standard"
        onChange={(event: React.ChangeEvent<HTMLInputElement>) => {
          contactDetailsStore.setNameContact(event.target.value);
        }}
        value={contactDetailsStore.newContact.name}
      />
    );
  });

  const ListPhoneNumber = observer(() => {
    if (!contactDetailsStore.newContact.phoneNumbers) {
      return;
    }
    return (
      <Box>
        {contactDetailsStore.newContact.phoneNumbers.map((value, index) => {
          return (
            <Box key={'BoxPhoneNumber' + index} sx={flexRow}>
              <TextField
                margin="dense"
                key={'PhoneNumber' + index}
                type="text"
                fullWidth
                variant="standard"
                onChange={(event: React.ChangeEvent<HTMLInputElement>) => {
                  contactDetailsStore.setPhoneNumber(index, event.target.value);
                }}
                value={value.number}
              />
              <IconButton
                key={'buttonDelNumber' + index}
                onClick={() =>
                  confirmationStore.open(
                    () => contactDetailsStore.deletePhoneNumber(index),
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

  const ListEmails = observer(() => {
    if (!contactDetailsStore.newContact.mails) {
      return;
    }
    return (
      <Box>
        {contactDetailsStore.newContact.mails.map((value, index) => {
          return (
            <Box key={'BoxPhoneNumber' + index} sx={flexRow}>
              <TextField
                margin="dense"
                key={'email' + index}
                type="text"
                fullWidth
                variant="standard"
                onChange={(event: React.ChangeEvent<HTMLInputElement>) => {
                  contactDetailsStore.setMailContact(index, event.target.value);
                }}
                value={value.email}
              />
              <IconButton
                key={'buttonDelEmail' + index}
                onClick={() =>
                  confirmationStore.open(
                    () => contactDetailsStore.deleteMail(index),
                    'Удалить e-mail - ' + value.email + '?'
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

  const DescriptionTextField = observer(() => {
    return (
      <TextField
        margin="dense"
        id="inputDescription"
        type="text"
        fullWidth
        variant="standard"
        onChange={(event: React.ChangeEvent<HTMLInputElement>) => {
          contactDetailsStore.setDescriptionContact(event.target.value);
        }}
        value={contactDetailsStore.newContact.description}
      />
    );
  });

  return (
    <Box sx={contactCard}>
      <AvatarCostom />
      <NameTextField />

      <Divider sx={styleDivider} />

      <Box sx={flexRow}>
        <IconButton disabled>
          <EditIcon />
        </IconButton>

        <IconButton onClick={() => contactDetailsStore.save()}>
          <SaveIcon />
        </IconButton>

        <IconButton onClick={() => contactDetailsStore.SaveCancel()}>
          <CancelIcon />
        </IconButton>
      </Box>

      <Divider sx={styleDivider} />

      <Box sx={flexRow}>
        <SvgIcon fontSize="small" component={PhoneIcon} sx={styleIcon} />

        <Box key={'PhoneNumberInput'} sx={flexRow}>
          <ListPhoneNumber />
          <IconButton onClick={() => contactDetailsStore.addPhoneNumber()}>
            <AddIcon />
          </IconButton>
        </Box>
      </Box>

      <Divider sx={styleDivider} />

      <Box sx={flexRow}>
        <SvgIcon fontSize="small" component={EmailIcon} sx={styleIcon} />

        <Box key={'MailInput'} sx={flexRow}>
          <ListEmails />
          <IconButton onClick={() => contactDetailsStore.addMail()}>
            <AddIcon />
          </IconButton>
        </Box>
      </Box>
      <Divider sx={styleDivider} />

      <DescriptionTextField />
    </Box>
  );
}
