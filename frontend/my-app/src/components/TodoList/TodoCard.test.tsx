import { render, screen, fireEvent } from "@testing-library/react";
import TodoCard from "./TodoCard";
import { Provider } from "react-redux";
import configureStore from "redux-mock-store";
import {  setSelectedTodo } from "../../features/todos/todosSlice";

const mockStore = configureStore([]);

jest.mock("react-redux", () => ({
    ...jest.requireActual("react-redux"),
    useDispatch: () => jest.fn(),
}));

describe("TodoCard", () => {
    let store: any;
    let dispatchMock: jest.Mock;

    const todo = {
        id: 1,
        title: "Test Todo",
        status: 0,
    };

    beforeEach(() => {
        dispatchMock = jest.fn();
        jest.spyOn(require("react-redux"), "useDispatch").mockReturnValue(dispatchMock);

        store = mockStore({});
    });

    const renderComponent = () =>
        render(
            <Provider store={store}>
                <TodoCard todo={todo} />
            </Provider>
        );

    it("Render new task", () => {
        renderComponent();
        expect(screen.getByText("Test Todo")).toBeInTheDocument();
    });



    it("call setSelectedTodo when click on arrow button", () => {
        renderComponent();
        const arrowButton = screen.getByTestId("arrow-button");

        fireEvent.click(arrowButton);

        expect(dispatchMock).toHaveBeenCalledWith(setSelectedTodo(todo));
    });


});
