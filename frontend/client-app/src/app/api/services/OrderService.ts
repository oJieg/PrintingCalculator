/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { BooleanAnswer } from '../models/BooleanAnswer';
import type { ContactAnswer } from '../models/ContactAnswer';
import type { Int32Answer } from '../models/Int32Answer';
import type { OrderAnswer } from '../models/OrderAnswer';
import type { OrderArrayAnswer } from '../models/OrderArrayAnswer';
import type { ProductAnswer } from '../models/ProductAnswer';
import type { StatusOrder } from '../models/StatusOrder';
import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';
export class OrderService {
    /**
     * @returns Int32Answer Success
     * @throws ApiError
     */
    public static getApiOrderAddNewOrder(): CancelablePromise<Int32Answer> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/order/add-new-order',
        });
    }
    /**
     * @param id
     * @returns OrderAnswer Success
     * @throws ApiError
     */
    public static getApiOrderGetOrder(
        id: number,
    ): CancelablePromise<OrderAnswer> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/order/get-order{Id}',
            path: {
                'Id': id,
            },
        });
    }
    /**
     * @param data
     * @returns OrderArrayAnswer Success
     * @throws ApiError
     */
    public static getApiOrderGetOrderData(
        data?: string,
    ): CancelablePromise<OrderArrayAnswer> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/order/get-order-data',
            query: {
                'data': data,
            },
        });
    }
    /**
     * @param statusOrder
     * @param skip
     * @param take
     * @returns OrderArrayAnswer Success
     * @throws ApiError
     */
    public static getApiOrderGetListOrder(
        statusOrder?: StatusOrder,
        skip?: number,
        take: number = 5,
    ): CancelablePromise<OrderArrayAnswer> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/order/get-list-order',
            query: {
                'statusOrder': statusOrder,
                'skip': skip,
                'take': take,
            },
        });
    }
    /**
     * @param skip
     * @param take
     * @returns OrderArrayAnswer Success
     * @throws ApiError
     */
    public static getApiOrderGetListCloseOrder(
        skip?: number,
        take: number = 5,
    ): CancelablePromise<OrderArrayAnswer> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/order/get-list-close-order',
            query: {
                'skip': skip,
                'take': take,
            },
        });
    }
    /**
     * @param statusOrder
     * @returns Int32Answer Success
     * @throws ApiError
     */
    public static getApiOrderGetCountOrder(
        statusOrder?: StatusOrder,
    ): CancelablePromise<Int32Answer> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/order/get-count-order',
            query: {
                'statusOrder': statusOrder,
            },
        });
    }
    /**
     * @param id
     * @param status
     * @returns BooleanAnswer Success
     * @throws ApiError
     */
    public static getApiOrderEditStatusOrder(
        id: number,
        status?: StatusOrder,
    ): CancelablePromise<BooleanAnswer> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/order/edit-status-order{Id}',
            path: {
                'Id': id,
            },
            query: {
                'status': status,
            },
        });
    }
    /**
     * @param id
     * @param description
     * @returns BooleanAnswer Success
     * @throws ApiError
     */
    public static getApiOrderEditDescriptionOrder(
        id: number,
        description?: string,
    ): CancelablePromise<BooleanAnswer> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/order/edit-description-order{Id}',
            path: {
                'Id': id,
            },
            query: {
                'description': description,
            },
        });
    }
    /**
     * @param orderId
     * @param contactId
     * @returns ContactAnswer Success
     * @throws ApiError
     */
    public static postApiOrderAddContact(
        orderId: number,
        contactId?: number,
    ): CancelablePromise<ContactAnswer> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/order/add-contact{orderId}',
            path: {
                'orderId': orderId,
            },
            query: {
                'contactId': contactId,
            },
        });
    }
    /**
     * @param orderId
     * @param contactId
     * @returns BooleanAnswer Success
     * @throws ApiError
     */
    public static deleteApiOrderDelContact(
        orderId: number,
        contactId?: number,
    ): CancelablePromise<BooleanAnswer> {
        return __request(OpenAPI, {
            method: 'DELETE',
            url: '/api/order/del-contact{orderId}',
            path: {
                'orderId': orderId,
            },
            query: {
                'contactId': contactId,
            },
        });
    }
    /**
     * @param orderId
     * @param productId
     * @returns ProductAnswer Success
     * @throws ApiError
     */
    public static postApiOrderAddProduct(
        orderId: number,
        productId?: number,
    ): CancelablePromise<ProductAnswer> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/order/add-product{orderId}',
            path: {
                'orderId': orderId,
            },
            query: {
                'productId': productId,
            },
        });
    }
    /**
     * @param orderId
     * @param productId
     * @returns BooleanAnswer Success
     * @throws ApiError
     */
    public static deleteApiOrderDelProduct(
        orderId: number,
        productId?: number,
    ): CancelablePromise<BooleanAnswer> {
        return __request(OpenAPI, {
            method: 'DELETE',
            url: '/api/order/del-product{orderId}',
            path: {
                'orderId': orderId,
            },
            query: {
                'productId': productId,
            },
        });
    }
}
