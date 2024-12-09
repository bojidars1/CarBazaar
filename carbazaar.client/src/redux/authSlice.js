import { createSlice } from "@reduxjs/toolkit";

const name = 'auth';
const initialState = createIntialState();
const reducers = createReducers();
const slice = createSlice({ name, initialState, reducers });

function createIntialState() {
    return {
        token: null
    }
}

function createReducers() {
    return {
        setAuthenticated: (state, action) => {
            state.token = action.payload;
        },
        logout: (state) => {
            state.token = null;
        }
    }
}

export const { setAuthenticated, logout } = slice.actions;

export default slice.reducer;