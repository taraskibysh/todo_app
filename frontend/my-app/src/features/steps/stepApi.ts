import axios from 'axios';
import type {Step} from './Step';

const URL = "https://localhost:7071/api/TodoItem";

export const DeleteStep = async ({ todoId, id }: { todoId: number; id: number }) => {
    console.log(`${URL}/${todoId}/Step/${id}`);
    await axios.delete(`${URL}/${todoId}/Step/${id}`);
    return { todoId, id };
};

export const AddStep = async ({todoId, step} :{todoId: number, step : Partial<Step>})  => (await axios.post(`${URL}/${todoId}/Step`, step)).data;