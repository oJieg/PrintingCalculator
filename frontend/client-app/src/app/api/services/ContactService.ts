/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { BooleanAnswer } from '../models/BooleanAnswer';
import type { Contact } from '../models/Contact';
import type { ContactAnswer } from '../models/ContactAnswer';
import type { ContactArrayAnswer } from '../models/ContactArrayAnswer';
import type { Int32Answer } from '../models/Int32Answer';
import type { OrderArrayAnswer } from '../models/OrderArrayAnswer';
import type { SearchContactsAnswer } from '../models/SearchContactsAnswer';
import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';
export class ContactService {
    /**
     * @param requestBody
     * @returns BooleanAnswer Success
     * @throws ApiError
     */
    public static postApiContactEditContact(
        requestBody?: Contact,
    ): CancelablePromise<BooleanAnswer> {
        return __request(OpenAPI, {
            method: 'POST',
            url: 'https://localhost:7181/api/contact/edit-contact',
            body: requestBody,
            mediaType: 'application/json',
        });
    }
    /**
     * @param requestBody
     * @returns BooleanAnswer Success
     * @throws ApiError
     */
    public static postApiContactAddNewContact(
        requestBody?: Contact,
    ): CancelablePromise<BooleanAnswer> {
        return __request(OpenAPI, {
            method: 'POST',
            url: 'https://localhost:7181/api/contact/add-new-contact',
            body: requestBody,
            mediaType: 'application/json',
        });
    }
    /**
     * @param contactId
     * @returns ContactAnswer Success
     * @throws ApiError
     */
    public static getApiContactGetContact(
        contactId: number,
    ): CancelablePromise<ContactAnswer> {
        return __request(OpenAPI, {
            method: 'GET',
            url: 'https://localhost:7181/api/contact/get-contact{contactId}',
            path: {
                'contactId': contactId,
            },
        });
    }
    /**
     * @param take
     * @param skip
     * @returns ContactArrayAnswer Success
     * @throws ApiError
     */
    public static postApiContactGetListContact(
        take?: number,
        skip?: number,
    ): CancelablePromise<ContactArrayAnswer> {
        return __request(OpenAPI, {
            method: 'POST',
            url: 'https://localhost:7181/api/contact/get-list-contact',
            query: {
                'take': take,
                'skip': skip,
            },
        });
    }
    /**
     * @returns Int32Answer Success
     * @throws ApiError
     */
    public static getApiContactGetCountContact(): CancelablePromise<Int32Answer> {
        return __request(OpenAPI, {
            method: 'GET',
            url: 'https://localhost:7181/api/contact/get-count-contact',
        });
    }
    /**
     * @param contactId
     * @returns OrderArrayAnswer Success
     * @throws ApiError
     */
    public static getApiContactGetAllOrderForContact(
        contactId: number,
    ): CancelablePromise<OrderArrayAnswer> {
        return __request(OpenAPI, {
            method: 'GET',
            url: 'https://localhost:7181/api/contact/get-all-order-for-contact{contactId}',
            path: {
                'contactId': contactId,
            },
        });
    }
    /**
     * @param search
     * @param take
     * @param skip
     * @returns SearchContactsAnswer Success
     * @throws ApiError
     */
    public static postApiContactSearthContact(
        search?: string,
        take?: number,
        skip?: number,
    ): CancelablePromise<SearchContactsAnswer> {
        return __request(OpenAPI, {
            method: 'POST',
            url: 'https://localhost:7181/api/contact/searth-contact',
            query: {
                'search': search,
                'take': take,
                'skip': skip,
            },
        });
    }
    /**
     * @param email
     * @returns ContactArrayAnswer Success
     * @throws ApiError
     */
    public static getApiContactSearthContactByEmail(
        email?: string,
    ): CancelablePromise<ContactArrayAnswer> {
        return __request(OpenAPI, {
            method: 'GET',
            url: 'https://localhost:7181/api/contact/searth-contact-by-email',
            query: {
                'email': email,
            },
        });
    }
    /**
     * @param name
     * @returns ContactArrayAnswer Success
     * @throws ApiError
     */
    public static getApiContactSearthContactByName(
        name?: string,
    ): CancelablePromise<ContactArrayAnswer> {
        return __request(OpenAPI, {
            method: 'GET',
            url: 'https://localhost:7181/api/contact/searth-contact-by-name',
            query: {
                'name': name,
            },
        });
    }
}
