
import { render, screen } from "@testing-library/react";
import TodoBoard from "./TodoBoard";
import { Provider } from "react-redux";
import configureStore from "redux-mock-store";
import dayjs from "dayjs";

const mockStore = configureStore([]);

describe("TodoBoard", () => {
    let store: any;

    beforeEach(() => {
        store = mockStore({
            todos: {
                selectedItem: null,
            },
        });
    });

    const renderWithStore = (store: any) =>
        render(
            <Provider store={store}>
                <TodoBoard />
            </Provider>
        );

    it("show message if task didnt chosen", () => {
        renderWithStore(store);

        expect(screen.getByText(/choose a task/i)).toBeInTheDocument();
    });

    it("show information about chosen task", () => {
        const todo = {
            id: "1",
            title: "Test Task",
            description: "Test Description",
            status: 1,
            deadline: dayjs(),
            steps: [{ id: "step1", text: "Step 1", done: false }],
        };

        store = mockStore({
            todos: {
                selectedItem: todo,
            },
        });

        renderWithStore(store);

        expect(screen.getByText(todo.title)).toBeInTheDocument();
        expect(screen.getByText("Description")).toBeInTheDocument();
        expect(screen.getByText(todo.description)).toBeInTheDocument();
        expect(screen.getByText("In Progress")).toBeInTheDocument();
        expect(screen.getByText("Task steps")).toBeInTheDocument();
    });

});
