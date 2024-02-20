import { makeObservable, observable, action, runInAction, toJS } from 'mobx';
import { Contact } from '../api/models/Contact';
import { ContactService } from '../api/services/ContactService';
import { alertStore } from './alert-store';

export class ContactDetailsStore {
  isOpen: boolean = false;
  contact: Contact = {};

  editeMod: boolean = false;
  newContact: Contact = {};

  openProgressBar: boolean = true;
  failSerser: boolean = false;

  constructor() {
    makeObservable(this, {
      isOpen: observable,
      contact: observable,
      editeMod: observable,
      openProgressBar: observable,
      failSerser: observable,
      newContact: observable,

      open: action,
      close: action,
      save: action,
      onEditMod: action,
      SaveCancel: action,

      setNameContact: action,
      setDescriptionContact: action,
      setMailContact: action,
      setPhoneNumber: action,
      deleteMail: action,
      deletePhoneNumber: action,
      addMail: action,
      addPhoneNumber: action
    });
    // autorun(()=>console.log(this.editeMod))
  }

  async open(idContact?: number) {
    if (!idContact) {
      return;
    }

    this.isOpen = true;
    try {
      const dataContact = await ContactService.getApiContactGetContact(idContact);
      runInAction(() => {
        if (!dataContact.result) {
          alertStore.openAlert('error', 'Ошибка получения контакта');
          return;
        }

        this.contact = dataContact.result;
        this.newContact = dataContact.result;
        console.log(this.newContact);
        console.log(this.contact);
        this.openProgressBar = false;
        return;
      });
    } catch (err) {
      runInAction(() => {
        this.failSerser = true;
        return;
      });
    }
  }

  close() {
    this.isOpen = false;
  }

  async save() {
    const answer = await ContactService.postApiContactEditContact(this.newContact);
    runInAction(() => {
      if (answer.result) {
        alertStore.openAlert('success', 'Сохранено');
        this.contact = toJS(this.newContact);
        this.editeMod = false;
      } else {
        alertStore.openAlert('error', 'Ошибка сохранения');
      }
    });
  }

  SaveCancel() {
    this.newContact = toJS(this.contact);
    this.editeMod = false;
  }

  onEditMod() {
    //this.newContact =  observable(this.contact);
    this.editeMod = true;
  }

  setNameContact(name: string) {
    this.newContact.name = name;
  }

  setDescriptionContact(description: string) {
    this.newContact.description = description;
  }

  setMailContact(index: number, email: string) {
    if (!this.newContact.mails) {
      alertStore.openAlert('error', 'Ошибка изменения');
      return;
    }
    this.newContact.mails[index].email = email;
  }

  setPhoneNumber(index: number, phoneNumber: string) {
    //валидация!!
    if (!this.newContact.phoneNumbers) {
      alertStore.openAlert('error', 'Ошибка изменения');
      return;
    }
    this.newContact.phoneNumbers[index].number = phoneNumber;
  }

  addMail() {
    if (!this.newContact.mails) {
      alertStore.openAlert('error', 'Ошибка добавления');
      return;
    }
    this.newContact.mails.push({ email: '' });
  }

  addPhoneNumber() {
    if (!this.newContact.phoneNumbers) {
      alertStore.openAlert('error', 'Ошибка добавления');
      return;
    }
    this.newContact.phoneNumbers.push({ number: '' });
  }

  deleteMail(index: number) {
    if (!this.newContact.mails) {
      alertStore.openAlert('error', 'Ошибка удаления');
      return;
    }
    this.newContact.mails.splice(index, 1);
  }

  deletePhoneNumber(index: number) {
    if (!this.newContact.phoneNumbers) {
      alertStore.openAlert('error', 'Ошибка удаления');
      return;
    }
    this.newContact.phoneNumbers.splice(index, 1);
  }
}

// export const contactDetailsStore = new ContactDetailsStore();
