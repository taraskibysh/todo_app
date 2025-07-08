import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import type { Todo } from './Todo.ts';
import * as api from './TodosApi';
import * as api2 from '../steps/stepApi.ts';

export const fetchTodos = createAsyncThunk('todos/fetch', api.fetchTodos);
export const addTodo = createAsyncThunk('todos/add', api.addTodo);
export const deleteTodo = createAsyncThunk('todos/delete', api.deleteTodo);
export const updateTodo = createAsyncThunk('todos/update', api.updateTodo);
export const addStep = createAsyncThunk("step/add", api2.AddStep );
export const deleteStep = createAsyncThunk("step/delete", api2.DeleteStep);
type TodosState = {
    items: Todo[];
    selectedItem?: Todo
};

const initialState: TodosState = {
    items: [],
    selectedItem : undefined
};

const todosSlice = createSlice({
    name: 'todos',
    initialState,
    reducers: {
        setSelectedTodo: (state, action) => {
            state.selectedItem = action.payload;
        }
    },
    extraReducers: (builder) => {
        builder
            .addCase(fetchTodos.fulfilled, (state, action) => {
                state.items = action.payload;
            })
            .addCase(addTodo.fulfilled, (state, action) => {
                state.items.push(action.payload);
            })
            .addCase(deleteTodo.fulfilled, (state, action) => {
                const index = state.items.findIndex((todo) => todo.id === action.payload);
                if (index !== -1) {
                    state.items.splice(index, 1);
                }
            })
            .addCase(updateTodo.fulfilled, (state, action) => {
                const index = state.items.findIndex((todo) => todo.id === action.payload.id);
                if (index !== -1) state.items[index] = action.payload;


            })
            .addCase(addStep.fulfilled, (state, action) => {
                const newStep = action.payload;
                const item  = state.items.find((item) => item.id == newStep.todoItemId)

                if (item) {
                    item.steps = [...(item.steps || []), newStep];
                }
                if(item && state.selectedItem && state.selectedItem.id == item.id ) {
                    state.selectedItem = item;
                }
            })
            .addCase(deleteStep.fulfilled, (state, action) => {
                const { todoId, id } = action.payload;
                console.log(todoId, id);
                const todo = state.items.find((item) => item.id === todoId);

                console.log(todo)
                if (todo && todo.steps) {
                    todo.steps = todo.steps.filter((step) => step.id != id);
                }
                if (state.selectedItem && todo && todo.id === state.selectedItem.id){
                    state.selectedItem = { ...todo };
                }
            });


    },
});

export default todosSlice.reducer;
export const { setSelectedTodo } = todosSlice.actions;
