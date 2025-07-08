// StepsList.test.tsx
import { render, screen, fireEvent } from '@testing-library/react';
import { StepsList } from './StepList';
import { Provider } from 'react-redux';
import configureStore from 'redux-mock-store';
import {addStep, deleteStep} from '../../features/todos/todosSlice';
import { thunk } from 'redux-thunk';
import userEvent from '@testing-library/user-event';



jest.mock('../../features/todos/todosSlice', () => {
    const originalModule = jest.requireActual('../../features/todos/todosSlice');
    return {
        ...originalModule,
        addStep: jest.fn(),
        deleteStep: jest.fn(),
    };
});

const mockStore = configureStore([thunk]);

const steps = [
    { id: 1, title: 'Step 1', isDone: false },
    { id: 2, title: 'Step 2', isDone: false },
];

describe('StepsList', () => {
    let store: any;

    beforeEach(() => {
        store = mockStore({});
        store.dispatch = jest.fn();
    });

    const renderComponent = () =>
        render(
            <Provider store={store}>
                <StepsList id={123} steps={steps} />
            </Provider>
        );

    it('renders steps', () => {
        renderComponent();
        expect(screen.getByText('Step 1')).toBeInTheDocument();
        expect(screen.getByText('Step 2')).toBeInTheDocument();
    });

    it('updates text field when typing', async () => {
        renderComponent();
        const input = screen.getByLabelText(/new step/i);
        await userEvent.type(input, 'New step');
        expect(input).toHaveValue('New step');
    });

    it('dispatches addStep on icon button click', async () => {
        renderComponent();
        const input = screen.getByLabelText(/new step/i);
        const addButton = screen.getByRole('button');

        await userEvent.type(input, 'Buy milk');
        fireEvent.click(addButton);

        expect(store.dispatch).toHaveBeenCalled();
        expect(addStep).toHaveBeenCalledWith({
            todoId: 123,
            step: {
                title: 'Buy milk',
                isDone: false,
            },
        });
    });

    it('calls onDeleteStep when checkbox clicked', () => {
        renderComponent();
        const checkboxes = screen.getAllByRole('checkbox');
        fireEvent.click(checkboxes[0]);

        expect(store.dispatch).toHaveBeenCalled();
        expect(deleteStep).toHaveBeenCalledWith({ todoId: 123, id: 1 });
    });
});
