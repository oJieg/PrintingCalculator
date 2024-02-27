import { makeObservable, observable, action, runInAction } from 'mobx';
import { Contact } from '../../api/models/Contact';
import { ContactService } from '../../api/services/ContactService';
import { alertStore } from '../alert-store';
import { Mail } from '../../api/models/Mail';
import { PhoneNumber } from '../../api/models/PhoneNumber';

export class NewContactStore {
  isOpen: boolean = false;
  nameContact: string = '';
  descriptionContact: string = '';
  mailsContact: Mail[] = [{ email: '' }];
  phoneNumbersContact: PhoneNumber[] = [{ number: '' }];

  constructor() {
    makeObservable(this, {
      isOpen: observable,
      nameContact: observable,
      descriptionContact: observable,
      mailsContact: observable,
      phoneNumbersContact: observable,

      setNameContact: action,
      setDescriptionContact: action,
      setMailContact: action,
      setPhoneNumber: action,
      deleteMail: action,
      deletePhoneNumber: action,
      addMail: action,
      addPhoneNumber: action,

      saveData: action,
      open: action,
      close: action
    });
  }
  setNameContact(name: string) {
    this.nameContact = name;
  }

  setDescriptionContact(description: string) {
    this.descriptionContact = description;
  }

  setMailContact(index: number, email: string) {
    this.mailsContact[index].email = email;
  }

  setPhoneNumber(index: number, phoneNumber: string) {
    //валидация!!
    this.phoneNumbersContact[index].number = phoneNumber;
  }

  addMail() {
    this.mailsContact.push({ email: '' });
  }

  addPhoneNumber() {
    this.phoneNumbersContact.push({ number: '' });
  }

  deleteMail(index: number) {
    this.mailsContact.splice(index, 1);
  }

  deletePhoneNumber(index: number) {
    this.phoneNumbersContact.splice(index, 1);
  }

  async saveData() {
    const newContact: Contact = {
      name: this.nameContact,
      description: this.descriptionContact,
      mails: this.mailsContact,
      phoneNumbers: this.phoneNumbersContact
    };
    try {
      const response = await ContactService.postApiContactAddNewContact(newContact);
      runInAction(() => {
        if (response.result) {
          alertStore.openAlert('success', 'Сохранено');
          this.isOpen = false;
          return;
        }
        alertStore.openAlert('error', 'ошибка сохранения');
      });
    } catch (ex) {
      alertStore.openAlert('error', 'ошибка сохранения');
    }
  }

  open() {
    this.isOpen = true;
  }

  close() {
    this.isOpen = false;
    this.nameContact = '';
    this.descriptionContact = '';
    this.mailsContact = [{ email: '' }];
    this.phoneNumbersContact = [{ number: '' }];
  }
}

// export const newContactStore = new NewContactStore();
