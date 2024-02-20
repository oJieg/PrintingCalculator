import { AlertColor } from '@mui/material/Alert';

export interface AlertState {
  open: boolean;
  severity?: AlertColor;
  massage?: string;
}
