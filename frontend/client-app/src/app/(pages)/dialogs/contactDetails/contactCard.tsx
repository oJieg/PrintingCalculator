import Box from '@mui/material/Box';
import Skeleton from '@mui/material/Skeleton';
import Paper from '@mui/material/Paper';

import { observer } from 'mobx-react-lite';
import { UseShowcaseStore } from '../../contacts/page';

import ContactCardEditing from './сontactCardEditing';
import ContactCardViewing from './contactCardViewing';

import { contactCard } from './cardContactStyle';

export default function ContactCard() {
  const { contactDetailsStore } = UseShowcaseStore();

  const CardContact = observer(() => {
    if (contactDetailsStore.editeMod) {
      return <ContactCardEditing />;
    }

    if (contactDetailsStore.openProgressBar) {
      return (
        <Box sx={contactCard}>
          <Skeleton variant="circular" width={45} height={45} />
          <Skeleton variant="rectangular" width={262} height={230} sx={{ my: 2 }} />
        </Box>
      );
    }

    return <ContactCardViewing />;
  });

  return (
    <Paper elevation={4} sx={contactCard}>
      <CardContact />
    </Paper>
  );
}
