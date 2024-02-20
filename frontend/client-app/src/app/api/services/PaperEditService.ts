/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { AddPaper } from '../models/AddPaper';
import type { EditPaper } from '../models/EditPaper';
import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';
export class PaperEditService {
    /**
     * @param requestBody
     * @returns boolean Success
     * @throws ApiError
     */
    public static postApiPaperEdit(
        requestBody?: AddPaper,
    ): CancelablePromise<boolean> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/PaperEdit',
            body: requestBody,
            mediaType: 'application/json',
        });
    }
    /**
     * @param requestBody
     * @returns boolean Success
     * @throws ApiError
     */
    public static putApiPaperEdit(
        requestBody?: EditPaper,
    ): CancelablePromise<boolean> {
        return __request(OpenAPI, {
            method: 'PUT',
            url: '/api/PaperEdit',
            body: requestBody,
            mediaType: 'application/json',
        });
    }
    /**
     * @param id
     * @returns boolean Success
     * @throws ApiError
     */
    public static deleteApiPaperEdit(
        id: number,
    ): CancelablePromise<boolean> {
        return __request(OpenAPI, {
            method: 'DELETE',
            url: '/api/PaperEdit/{id}',
            path: {
                'id': id,
            },
        });
    }
}
