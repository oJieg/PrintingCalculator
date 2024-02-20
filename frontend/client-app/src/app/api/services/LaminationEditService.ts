/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { AddLamination } from '../models/AddLamination';
import type { EditLamination } from '../models/EditLamination';
import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';
export class LaminationEditService {
    /**
     * @param requestBody
     * @returns boolean Success
     * @throws ApiError
     */
    public static postApiLaminationEdit(
        requestBody?: AddLamination,
    ): CancelablePromise<boolean> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/LaminationEdit',
            body: requestBody,
            mediaType: 'application/json',
        });
    }
    /**
     * @param requestBody
     * @returns boolean Success
     * @throws ApiError
     */
    public static putApiLaminationEdit(
        requestBody?: EditLamination,
    ): CancelablePromise<boolean> {
        return __request(OpenAPI, {
            method: 'PUT',
            url: '/api/LaminationEdit',
            body: requestBody,
            mediaType: 'application/json',
        });
    }
    /**
     * @param id
     * @returns boolean Success
     * @throws ApiError
     */
    public static deleteApiLaminationEdit(
        id: number,
    ): CancelablePromise<boolean> {
        return __request(OpenAPI, {
            method: 'DELETE',
            url: '/api/LaminationEdit/{id}',
            path: {
                'id': id,
            },
        });
    }
}
