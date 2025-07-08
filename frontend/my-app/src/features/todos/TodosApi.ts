import axios from 'axios';
import type { Todo } from './Todo.ts';

const API_URL = 'https://localhost:7071/api/TodoItem';

export const fetchTodos = async () => (await axios.get(API_URL)).data;
export const addTodo = async (todo: Partial<Todo>) => (await axios.post(API_URL, todo)).data;
export const deleteTodo = async (id: number) => {
    await axios.delete(`${API_URL}/${id}`);
    return id;
};
export const updateTodo = async (todo: Todo) => (await axios.put(`${API_URL}/${todo.id}`, todo)).data;
