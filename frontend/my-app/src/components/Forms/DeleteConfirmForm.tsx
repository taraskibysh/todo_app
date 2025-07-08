import {useDispatch, useSelector} from "react-redux";
import type {RootState, AppDispatch} from "../../store/Store";
import {Button} from "@mui/material";
import {deleteTodo, setSelectedTodo} from "../../features/todos/todosSlice.ts";
import  "./CreateForm.css";


export interface DeleteConfirmFormProps {
    id?: number;
    onClose : () => void;
}

export function DeleteConfirmForm(props: DeleteConfirmFormProps) {

    const dispatch = useDispatch<AppDispatch>();
    const selectedTodo = useSelector((state: RootState) => state.todos.selectedItem);

    const deleteHandle = () => {

        if (selectedTodo != undefined && selectedTodo.id == props.id) {
                dispatch(setSelectedTodo(undefined));
        }

        if (props.id != undefined) {
            dispatch(deleteTodo(props.id));
        }

        props.onClose();


    }

    return (
        <div className="modal-center-wrapper">
            <div className="centered-container">
        <h1>Do you really want to delete this object?</h1>
            <form>


                <Button sx={{backgroundColor: '#743acc', marginRight: 3 }} variant="contained" onClick={props.onClose}>
                    Cancel
                </Button>
                <Button onClick={deleteHandle} sx={{backgroundColor: "red", marginRight: 3 }} variant="contained">
                    Confirm
                </Button >

    </form>
            </div>
        </div>
    )


}