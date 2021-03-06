import React, { useState, useContext, FormEvent } from "react";
import { useHistory } from "react-router-dom";
import { AppContext } from "../../context/AppContext";
import { IRegisterDTO } from "../../domain/identity/IRegisterDTO";
import { AccountApi } from "../../services/AccountApi";

const Register = () => {
    const history = useHistory();
    const [props, setState] = useState({} as IRegisterDTO);
    const appContext = useContext(AppContext);

    const handleChange = (target: EventTarget & HTMLInputElement) => {
        setState({ ...props, [target.name]: target.value });
    }

    const handleSubmit = (e: FormEvent<HTMLFormElement>) => {
        let jwt: string | null = null;
        AccountApi.register(props).then(data => {
            if (data === 200) {
                history.push('/login');
            }
        });


        e.preventDefault();
    }

    return (
        <>
            <div className="container">
            <h1>Registreeri</h1>
                <div className="row">
                    <div className="col-md-4">

                        <form onSubmit={handleSubmit}>
                            <label>Meiliaadress</label>
                            <input
                                onChange={(e) => handleChange(e.target)}
                                className="form-control"
                                type="email"
                                name="email"
                            />
                            <label >Parool</label>
                            <input
                                onChange={(e) => handleChange(e.target)}
                                className="form-control"
                                type="password"
                                name="password"
                            />
                Eesnimi
                <input
                                onChange={(e) => handleChange(e.target)}
                                className="form-control"
                                type="text"
                                name="firstName"
                            />
                Perenimi
                <input
                                onChange={(e) => handleChange(e.target)}
                                className="form-control"
                                type="text"
                                name="lastName"
                            />
                            <button className="btn button-bg-purple-dark" type="submit">Registreeru</button>
                        </form>
                    </div>
                </div>
            </div>
        </>
    );
}
export default Register;