import { createSlice } from "@reduxjs/toolkit";

const name = 'user';
const initialState = createIntialState();
const reducers = createReducers();
const slice = createSlice({ name, initialState, reducers });

function createIntialState() {
    return {
        value: null
    }
}

function createReducers() {
    return {
        setUser: (state, action) => {
            state.user = action.payload;
        },
        clearUser: (state) => {
            state.user = null;
        }
    }
}

export const { setUser, clearUser } = slice.actions;

export default slice.reducer;