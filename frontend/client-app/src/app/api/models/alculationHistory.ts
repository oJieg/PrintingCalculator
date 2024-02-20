/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { ConsumablePrice } from './ConsumablePrice';
import type { InputHistory } from './InputHistory';
export type alculationHistory = {
    id?: number;
    dateTime?: string;
    input?: InputHistory;
    paperPrice?: number;
    consumablePrice?: ConsumablePrice;
    markupPaper?: number | null;
    cutPrice?: number | null;
    laminationPrices?: number | null;
    laminationMarkup?: number | null;
    creasingPrice?: number | null;
    drillingPrice?: number | null;
    roundingPrice?: number | null;
    springBrochurePrice?: number | null;
    stapleBrochurePrice?: number | null;
    price?: number | null;
    comment?: string | null;
};

