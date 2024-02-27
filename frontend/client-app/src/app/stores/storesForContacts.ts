import { ContactsStore } from './contacts/contacts-store';
import { NewContactStore } from './contacts/newContact-store';
import { ContactDetailsStore } from './contacts/contactDetails-store';
import { ConfirmationStore } from './confirmation-Store';

export class StoresForContacts {
  contactsStore = new ContactsStore();
  newContactStore = new NewContactStore();
  contactDetailsStore = new ContactDetailsStore();
  confirmationStore = new ConfirmationStore();
}
