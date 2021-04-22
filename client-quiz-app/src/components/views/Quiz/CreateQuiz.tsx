import { TextField } from "@material-ui/core";
import React, { useContext, useEffect, useState } from "react";
import { BaseService } from "../../../services/BaseService";
import { AppContext } from "../../../context/AppContext";
import { IQuizTopicWithCounter } from "../../../domain/custom/IQuizTopicDTO";
import { IQuizDTO } from "../../../domain/IQuizDTO";
import { IQuizTopicDTO } from "../../../domain/IQuizTopicDTO";


const CreateQuiz = () => {
    const appContext = useContext(AppContext);
    const [quizData, setQuizData] = useState({ title: '', finished: false, start: new Date() , appUserId: appContext.data.appUserId} as IQuizDTO);


    function handleQuizInputChange(e: React.ChangeEvent<HTMLTextAreaElement | HTMLInputElement>) {
        const { name, value } = e.target;
        setQuizData({ ...quizData, [name]: value });
    };

    const createQuiz = async () => {
        await BaseService.createEntity<IQuizDTO>(quizData, 'quiz', appContext.data!.token!)
        .then(data => console.log(data))
    };

    return (
        <div className="container">
            <h3>Create a new quiz</h3>
            <div>
                <form>
                    <div>
                        <TextField
                            id="standard-basic"
                            label="Quiz name"
                            name="title"
                            placeholder="Enter quiz name"
                            type="text"
                            value={quizData.title}
                            onChange={e => handleQuizInputChange(e)} />
                    </div><p></p>
                    <div>
                        <TextField
                            id="datetime-local"
                            label="Start date"
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
               
                <button type="button" className="btn btn-success" onClick={createQuiz}>Add a question block</button>
            </div>
        </div>
    );
}

export default CreateQuiz;
