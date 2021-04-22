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
            <h1>Register</h1>
                <div className="row">
                    <div className="col-md-4">

                        <form onSubmit={handleSubmit}>
                            <label>Email</label>
                            <input
                                onChange={(e) => handleChange(e.target)}
                                className="form-control"
                                type="email"
                                name="email"
                            />
                            <label >Password</label>
                            <input
                                onChange={(e) => handleChange(e.target)}
                                className="form-control"
                                type="password"
                                name="password"
                            />
                First name
                <input
                                onChange={(e) => handleChange(e.target)}
                                className="form-control"
                                type="text"
                                name="firstName"
                            />
                Last name
                <input
                                onChange={(e) => handleChange(e.target)}
                                className="form-control"
                                type="text"
                                name="lastName"
                            />
                            <button className="btn btn-primary" type="submit">Register</button>
                        </form>
                    </div>
                </div>
            </div>
        </>
    );
}
export default Register;