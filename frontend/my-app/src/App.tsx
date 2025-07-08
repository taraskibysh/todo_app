import React from 'react';
import TodoApp from './components/TodoApp';
import './App.css';
import { Provider } from 'react-redux';
import {store} from "./store/Store.ts";

const App: React.FC = () => {
    return (

        <Provider store={store}>
            <h1 style = {{fontSize: 40, marginLeft: "auto", fontFamily: 'Comic Sans MS'}}>Todoshka</h1>
            <TodoApp  />
        </Provider>
    );
};

export default App;
