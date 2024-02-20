/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { ApiResultAnswer } from '../models/ApiResultAnswer';
import type { ApiSimplResultAnswer } from '../models/ApiSimplResultAnswer';
import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';
export class GetResultService {
    /**
     * @param id
     * @returns ApiResultAnswer Success
     * @throws ApiError
     */
    public static getApiGetResult(
        id: number,
    ): CancelablePromise<ApiResultAnswer> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/get-result{id}',
            path: {
                'id': id,
            },
        });
    }
    /**
     * @param id
     * @returns ApiSimplResultAnswer Success
     * @throws ApiError
     */
    public static getApiGetSimplResult(
        id: number,
    ): CancelablePromise<ApiSimplResultAnswer> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/get-simpl-result{id}',
            path: {
                'id': id,
            },
        });
    }
}
