import { makeObservable, observable, action} from 'mobx';

export class ConfirmationStore {
  isOpen: boolean = false;
  message: string = '';

  action: ()=>void = ()=>{
      return
  }

  constructor() {
    makeObservable(this, {
      isOpen: observable,
      message: observable,
      open:action,
      confirm:action,
      cancel:action
    });
  }

  open(action:()=>void, message:string){
    this.message = message;
    this.isOpen = true;
    this.action = action;
  }

  confirm(){
    this.action();
    this.isOpen=false;
  }

  cancel(){
    this.isOpen=false;
  }
}

// export const confirmationStore = new ConfirmationStore();
