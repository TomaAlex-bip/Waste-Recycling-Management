import { IContainer } from './IContainer';

export interface IRecyclingPoint {
    name: string;
    latitude: number;
    longitude: number;
    containers: IContainer[];
    errorMessage: any;
    id: number;
}