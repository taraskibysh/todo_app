import  {useEffect} from 'react';
import type { Todo } from '../../features/todos/Todo.ts';
import TodoCard from './TodoCard.tsx';
import { useSelector, useDispatch } from 'react-redux';
import { fetchTodos } from '../../features/todos/todosSlice.ts';
import type { RootState, AppDispatch } from '../../store/Store.ts';



const TodoList = () => {

    const todos = useSelector((state: RootState) => state.todos.items);
    const dispatch = useDispatch<AppDispatch>();
    useEffect(() => {
        dispatch(fetchTodos());
    }, [dispatch]);

    if (todos.length === 0) return <p>No todos yet</p>;
    return (
        <ul>
            {todos.map((todo: Todo) => {
                return (
                    <TodoCard
                        key={todo.id}
                        todo={todo}
                    />
                );
            })}
        </ul>
    );
};

export default TodoList;
