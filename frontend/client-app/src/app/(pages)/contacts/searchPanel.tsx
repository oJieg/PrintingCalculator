import Paper from '@mui/material/Paper';
import InputBase from '@mui/material/InputBase';
import IconButton from '@mui/material/IconButton';
import CloseIcon from '@mui/icons-material/Close';

import { observer } from 'mobx-react-lite';
import { UseShowcaseStore } from './page';

import { paperSearchPanel, iconCloseButtonForSearchPanel } from './styleContacts';

export default function SearchPanel() {
  const { contactsStore } = UseShowcaseStore();

  const InputSearch = observer(() => {
    return (
      <InputBase
        sx={{ ml: 1, flex: 1 }}
        placeholder="Search name/phone/email"
        inputProps={{ 'aria-label': 'search google mapsvv' }}
        value={contactsStore.searchContacts}
        onChange={(event: React.ChangeEvent<HTMLInputElement>) => {
          contactsStore.getSearchContacts(1, event.target.value);
          if (!event.target.value) {
            contactsStore.clearSearchContact();
            return;
          }
        }}
      />
    );
  });

  return (
    <Paper component="form" sx={paperSearchPanel}>
      <InputSearch />
      <IconButton
        type="button"
        sx={iconCloseButtonForSearchPanel}
        aria-label="search"
        onClick={() => contactsStore.clearSearchContact()}>
        <CloseIcon />
      </IconButton>
    </Paper>
  );
}
