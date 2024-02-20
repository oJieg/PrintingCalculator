/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { alculationHistory } from './alculationHistory';
export type Product = {
    id?: number;
    name?: string | null;
    description?: string | null;
    histories?: Array<alculationHistory> | null;
    activecalculationHistoryId?: number;
    price?: number | null;
};

