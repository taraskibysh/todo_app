import type {Step} from '../steps/Step.ts';

export interface Todo {
    id?: number;

    title: string;

    description?: string | null;

    deadline?: string;

    status: number;

    steps?: Step[];
}
