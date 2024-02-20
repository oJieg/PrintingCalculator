import { ContactsStore } from './contacts-store';
import { NewContactStore } from './newContact-store';
import { ContactDetailsStore } from './contactDetails-store';
import { ConfirmationStore } from './confirmation-Store';

export class ShowcaseStore {
  contactsStore = new ContactsStore();
  newContactStore = new NewContactStore();
  contactDetailsStore = new ContactDetailsStore();
  confirmationStore = new ConfirmationStore();
}
