/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { AddComment } from '../models/AddComment';
import type { ApiResultAnswer } from '../models/ApiResultAnswer';
import type { ApiSimplCalculationAnswer } from '../models/ApiSimplCalculationAnswer';
import type { Input } from '../models/Input';
import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';
export class CalculationService {
    /**
     * @param requestBody
     * @returns ApiSimplCalculationAnswer Success
     * @throws ApiError
     */
    public static postApiSimplCalculation(
        requestBody?: Input,
    ): CancelablePromise<ApiSimplCalculationAnswer> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/simpl-calculation',
            body: requestBody,
            mediaType: 'application/json',
        });
    }
    /**
     * @param requestBody
     * @returns boolean Success
     * @throws ApiError
     */
    public static putApiAddComment(
        requestBody?: AddComment,
    ): CancelablePromise<boolean> {
        return __request(OpenAPI, {
            method: 'PUT',
            url: '/api/add-comment',
            body: requestBody,
            mediaType: 'application/json',
        });
    }
    /**
     * @param requestBody
     * @returns ApiResultAnswer Success
     * @throws ApiError
     */
    public static postApiCalculation(
        requestBody?: Input,
    ): CancelablePromise<ApiResultAnswer> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/calculation',
            body: requestBody,
            mediaType: 'application/json',
        });
    }
}
