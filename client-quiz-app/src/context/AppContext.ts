import React from "react";
import { IJwtResponseDTO } from "../domain/identity/IJwtResponseDTO";


export interface IAppContext {
    data: IJwtResponseDTO;
    setData: (data: IJwtResponseDTO) => void;
}

export const AppContextInitialState: IAppContext = {
    data: {} as IJwtResponseDTO,
    setData: (x) => { },
}

export const AppContext = React.createContext<IAppContext>(AppContextInitialState);

export const AppContextProvider = AppContext.Provider;
export const AppContextConsumer = AppContext.Consumer;