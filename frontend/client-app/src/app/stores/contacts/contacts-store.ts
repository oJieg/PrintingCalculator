import { makeObservable, observable, action, runInAction } from 'mobx';
import { Contact } from '../../api/models/Contact';
import { ContactService } from '../../api/services/ContactService';
import { PaginationState } from '../../model/paginationState';
import { alertStore } from '../alert-store';

export class ContactsStore {
  contacts: Contact[] = [];
  paginationState: PaginationState = {
    currentPage: 1,
    allPage: 1,
    take: 5,
    currentPageForSearch: 1
  };
  searchContacts: string = '';
  openProgressBar: boolean = true;
  failSerser: boolean = false;

  constructor() {
    makeObservable(this, {
      contacts: observable,
      paginationState: observable,
      searchContacts: observable,
      openProgressBar: observable,
      failSerser: observable,

      getContacts: action,
      getSearchContacts: action,
      clearSearchContact: action,
      setCurrentPage: action
    });
    // autorun(()=> console.log(this.contacts.length, this.openProgressBar));
    //this.getContacts();
  }

  async getContacts(page: number = this.paginationState.currentPage) {
    // console.log("get")
    this.openProgressBar = true;
    if(this.searchContacts){
     this.getSearchContacts()
     return;
    }
    try {
      const countContacts = await ContactService.getApiContactGetCountContact();
      runInAction(() => {
        if (!countContacts || countContacts.status || !countContacts.result) {
          alertStore.openAlert('error', 'Ошибка получения контактов, обратитесь к администратору');
          return;
        }
        this.paginationState.allPage = Math.ceil(countContacts.result / this.paginationState.take);
      });

      const skip = page * this.paginationState.take - this.paginationState.take;
      const dataContact = await ContactService.postApiContactGetListContact(this.paginationState.take, skip);
      runInAction(() => {
        if (!dataContact || dataContact.status || !dataContact.result) {
          alertStore.openAlert('error', 'Ошибка получения контактов, обратитесь к администратору');
          return;
        }
        this.contacts = this.fillRestWithEmptiness(dataContact.result);

        this.openProgressBar = false;
      });
    } catch (ex) {
      runInAction(() => {
        this.failSerser = true;
        console.log(ex);
      });
    }
  }

  async getSearchContacts(page: number = this.paginationState.currentPageForSearch, seatch: string = this.searchContacts) {
    if (!seatch) {
      return;
    }
    if (seatch != this.searchContacts) {
      this.searchContacts = seatch;
    }
    this.openProgressBar = true;

    try {
      const skip = page * this.paginationState.take - this.paginationState.take;
      const searchAnswer = await ContactService.postApiContactSearthContact(
        seatch,
        this.paginationState.take,
        skip
      );
      runInAction(() => {
        if (searchAnswer.status == 4) {
          this.paginationState.allPage = 1;
          this.contacts = this.fillRestWithEmptiness();
          this.openProgressBar = false;
          return;
        }

        if (
          !searchAnswer ||
          searchAnswer.status ||
          !searchAnswer.result ||
          !searchAnswer.result.totalCount
        ) {
          alertStore.openAlert('error', 'Ошибка получения контактов, обратитесь к администратору');
          return;
        }

        this.paginationState.allPage = Math.ceil(searchAnswer.result.totalCount / this.paginationState.take);
        this.contacts = this.fillRestWithEmptiness(searchAnswer.result.contacts!);

        this.openProgressBar = false;
      });
    } catch (ex) {
      runInAction(() => {
        this.failSerser = true;
        console.log(ex);
      });
    }
  }

  clearSearchContact() {
    this.searchContacts = '';
    this.getContacts();
  }

  setCurrentPage(currentPage: number) {
    if (this.searchContacts) {
      this.paginationState.currentPageForSearch = currentPage
      this.getSearchContacts(currentPage);
      return;
    }
    this.paginationState.currentPage = currentPage;
    this.getContacts(currentPage);
  }

  fillRestWithEmptiness(result?: Contact[]) {
    if (!result) {
      result = [{ id: -1 }];
    }
    const length = result.length;
    if (length < this.paginationState.take) {
      const contactEmpty: Contact = { id: -1 };
      for (let i = length; i < this.paginationState.take; i++) {
        result[i] = { id: -1 * (i + 1) };
      }
    }
    return result;
  }
}

 //export const contactsStore = new ContactsStore();
