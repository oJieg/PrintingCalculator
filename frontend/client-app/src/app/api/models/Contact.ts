/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { Mail } from './Mail';
import type { Order } from './Order';
import type { PhoneNumber } from './PhoneNumber';
export type Contact = {
    id?: number;
    name?: string | null;
    description?: string | null;
    mails?: Array<Mail> | null;
    phoneNumbers?: Array<PhoneNumber> | null;
    orders?: Array<Order> | null;
};

