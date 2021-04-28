import { TextField } from "@material-ui/core";
import React, { useContext, useEffect, useState } from "react";
import { BaseService } from "../../../services/BaseService";
import { AppContext } from "../../../context/AppContext";
import { IQuizTopicWithCounter } from "../../../domain/custom/IQuizTopicWithCounter";
import { IQuizDTO } from "../../../domain/IQuizDTO";
import { IQuizTopicDTO } from "../../../domain/IQuizTopicDTO";
import { useHistory } from "react-router-dom";


const CreateQuiz = () => {
    const appContext = useContext(AppContext);
    const history = useHistory();
    const [quizData, setQuizData] = useState({ title: '', finished: false, start: new Date() , appUserId: appContext.data.appUserId} as IQuizDTO);


    function handleQuizInputChange(e: React.ChangeEvent<HTMLTextAreaElement | HTMLInputElement>) {
        const { name, value } = e.target;
        setQuizData({ ...quizData, [name]: value });
    };

    const createQuiz = async () => {
        await BaseService.createEntity<IQuizDTO>(quizData, 'quiz', appContext.data!.token!)
        .then(data => {
            history.push("/editquiz/" + data?.id)
        })
    };

    return (
        <div className="container">
            <h3>Looge uus mälumäng</h3>
            <div>
                <form>
                    <div>
                        <TextField
                            id="standard-basic"
                            label="Mälumängu nimi"
                            name="title"
                            placeholder="Mälumängu nimi"
                            type="text"
                            value={quizData.title}
                            onChange={e => handleQuizInputChange(e)} />
                    </div><p></p>
                    <div>
                        <TextField
                            id="datetime-local"
                            label="Toimumise aeg"
                            name="start"
                            value={quizData.start}
                            onChange={e => handleQuizInputChange(e)}
                            InputLabelProps={{
                                shrink: true,
                            }}
                            type="datetime-local"
                        />
                    </div><p></p>
                </form>
               
                <button type="button" className="btn button-bg-purple-dark" onClick={createQuiz}>Loo mälumäng</button>
            </div>
        </div>
    );
}

export default CreateQuiz;
