/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';
export class VerySimpleCalculationService {
    /**
     * @param historyId
     * @param newAmount
     * @returns number Success
     * @throws ApiError
     */
    public static getApiVerySimpleCalculation(
        historyId: number,
        newAmount: number,
    ): CancelablePromise<number> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/VerySimpleCalculation/{historyId}/{newAmount}',
            path: {
                'historyId': historyId,
                'newAmount': newAmount,
            },
        });
    }
}
