/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { ProductAnswer } from '../models/ProductAnswer';
import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';
export class ProductService {
    /**
     * @param idProduction
     * @returns ProductAnswer Success
     * @throws ApiError
     */
    public static getApiProductGetProduct(
        idProduction: number,
    ): CancelablePromise<ProductAnswer> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/product/get-product{idProduction}',
            path: {
                'idProduction': idProduction,
            },
        });
    }
    /**
     * @param name
     * @param description
     * @returns ProductAnswer Success
     * @throws ApiError
     */
    public static getApiProductAddNewProduct(
        name?: string,
        description?: string,
    ): CancelablePromise<ProductAnswer> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/product/add-new-product',
            query: {
                'name': name,
                'description': description,
            },
        });
    }
    /**
     * @param productId
     * @param histiryId
     * @returns ProductAnswer Success
     * @throws ApiError
     */
    public static getApiProductAddHistory(
        productId: number,
        histiryId?: number,
    ): CancelablePromise<ProductAnswer> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/product/add-history{productId}',
            path: {
                'productId': productId,
            },
            query: {
                'histiryId': histiryId,
            },
        });
    }
    /**
     * @param productId
     * @param historyId
     * @returns ProductAnswer Success
     * @throws ApiError
     */
    public static getApiProductEditActiveHistory(
        productId?: number,
        historyId?: number,
    ): CancelablePromise<ProductAnswer> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/product/edit-active-history',
            query: {
                'productId': productId,
                'historyId': historyId,
            },
        });
    }
    /**
     * @param productId
     * @param name
     * @param description
     * @returns ProductAnswer Success
     * @throws ApiError
     */
    public static getApiProductEditProduct(
        productId?: number,
        name?: string,
        description?: string,
    ): CancelablePromise<ProductAnswer> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/product/edit-product',
            query: {
                'productId': productId,
                'name': name,
                'description': description,
            },
        });
    }
}
