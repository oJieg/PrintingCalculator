/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { Contact } from './Contact';
import type { Product } from './Product';
import type { StatusOrder } from './StatusOrder';
export type Order = {
    id?: number;
    description?: string | null;
    products?: Array<Product> | null;
    contacts?: Array<Contact> | null;
    dateTime?: string;
    status?: StatusOrder;
};

