/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { Lamination } from './Lamination';
import type { PaperCatalog } from './PaperCatalog';
import type { SpringBrochure } from './SpringBrochure';
export type InputHistory = {
    id?: number;
    height?: number;
    whidth?: number;
    paper?: PaperCatalog;
    amount?: number;
    kinds?: number;
    duplex?: boolean;
    laminationId?: number | null;
    lamination?: Lamination;
    creasingAmount?: number;
    drillingAmount?: number;
    roundingAmount?: boolean;
    commonToAllMarkupName?: Array<string> | null;
    springBrochure?: SpringBrochure;
    stapleBrochure?: boolean;
};

