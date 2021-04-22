import React, { useContext, useEffect, useState } from "react";
import { useHistory, useParams } from "react-router-dom";
import { BaseService } from "../../../services/BaseService";
import { AppContext } from "../../../context/AppContext";
import { IQuizDTO } from "../../../domain/IQuizDTO";


const UpcomingQuizView = () => {
    let { id } = useParams<{ id: string }>();
    const history = useHistory();
    const appContext = useContext(AppContext);
    const [user, setUser] = useState({} as IQuizDTO);


    const data = async () => {
        await BaseService
            .getEntity<IQuizDTO>(id, 'quiz', appContext.data.token)
            .then(data => {
                setUser(data!);
                console.log(data)

            });
    }

    useEffect(() => {
        data();
    }, []);

    return (
        <div className="container border rounded">
            {id}
        </div>
    )
}

export default UpcomingQuizView;
