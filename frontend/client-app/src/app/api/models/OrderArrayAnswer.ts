/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { Order } from './Order';
import type { StatusAnswer } from './StatusAnswer';
export type OrderArrayAnswer = {
    status?: StatusAnswer;
    errorMassage?: string | null;
    result?: Array<Order> | null;
};
