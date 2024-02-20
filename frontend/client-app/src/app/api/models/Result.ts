/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { CommonToAllMarkup } from './CommonToAllMarkup';
import type { LaminationResult } from './LaminationResult';
import type { PaperResult } from './PaperResult';
import type { PosResult } from './PosResult';
import type { SpringBrochureResult } from './SpringBrochureResult';
export type Result = {
    historyInputId?: number;
    amount?: number;
    kinds?: number;
    height?: number;
    whidth?: number;
    paperResult?: PaperResult;
    laminationResult?: LaminationResult;
    posResult?: PosResult;
    price?: number;
    tryPrice?: boolean;
    commonToAllMarkupName?: Array<CommonToAllMarkup> | null;
    tryCommonToAllMarkup?: Array<boolean> | null;
    dateTime?: string;
    comment?: string | null;
    springBrochure?: SpringBrochureResult;
};

