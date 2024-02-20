import { makeObservable, observable, action} from 'mobx';
import { AlertState } from '../model/alertState';
import { AlertColor } from '@mui/material';

export class AlertStore {
  alert: AlertState = {
    open: false
  };

  constructor() {
    makeObservable(this, {
      alert: observable,

      closeAlert: action,
      openAlert: action
    });
  }

  closeAlert() {
    this.alert.open = false;
  }

  openAlert(severity: AlertColor, massage: string) {
    this.alert = {
      open: true,
      severity: severity,
      massage: massage
    };
  }
}

export const alertStore = new AlertStore();
