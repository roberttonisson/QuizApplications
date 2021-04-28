import React, { useContext, useEffect, useState } from "react";
import { useHistory, useParams } from "react-router-dom";
import { BaseService } from "../../../services/BaseService";
import { AppContext } from "../../../context/AppContext";
import { IQuizDTO } from "../../../domain/IQuizDTO";
import { ITeamDTO } from "../../../domain/ITeamDTO";
import { IQuizTopicDTO } from "../../../domain/IQuizTopicDTO";
import { ITopicQuestionDTO } from "../../../domain/ITopicQuestionDTO";
import PointsModal from "../../shared/PointsModal";
import { ISavedQuestionDTO } from "../../../domain/ISavedQuestionDTO";



const FinishedQuizView = () => {
    let { id } = useParams<{ id: string }>();
    const history = useHistory();
    const appContext = useContext(AppContext);
    const [quiz, setQuiz] = useState({ teams: [] as ITeamDTO[], quizTopics: [] as IQuizTopicDTO[] } as IQuizDTO);
    const [currentBlock, setCurrentBlock] = useState({ topicQuestions: [] as ITopicQuestionDTO[] } as IQuizTopicDTO);
    const [savedQuestions, setSavedQuestions] = useState([] as ISavedQuestionDTO[]);


    const data = async () => {
        await BaseService
            .getEntity<IQuizDTO>(id, 'quiz', appContext.data.token)
            .then(data => {
                setQuiz(data!);
                setCurrentBlock(data!.quizTopics![0])
                console.log(data)
            });
        await BaseService
            .getEntities<ISavedQuestionDTO>('savedQuestions', appContext.data.token)
            .then(data => {
                setSavedQuestions(data)
                console.log(data)
            });
    }

    useEffect(() => {
        data();
    }, []);

    function selectTopic(topic: IQuizTopicDTO) {
        setCurrentBlock(topic);
    }

    function adminButtons() {
        if (appContext.data.appUserId === quiz.appUserId) {
            return (
                <>
                    <p className="text-center">_____</p>
                    <button type="button" className="btn button-bg-purple-dark btn-block" onClick={e => { }}>EDIT</button>
                </>
            )
        }

        return <></>
    }

    const saveQuestion = async (question: ITopicQuestionDTO) => {
        var entity =  {topicQuestionId: question.id, appUserId: appContext.data.appUserId} as ISavedQuestionDTO;
        await BaseService
            .createEntity<ISavedQuestionDTO>(entity,'savedQuestions', appContext.data.token)
            .then(data => {
                setSavedQuestions([...savedQuestions, data!])
            });
    }

    function saveQuestionButton(question: ITopicQuestionDTO) {
        if (savedQuestions.find(a => a.topicQuestionId === question.id)) {
            return <div className=" h6 d-inline font-weight-normal">Salvestatud</div>
        }

        return <button type="button" className="btn button-bg-purple-dark" onClick={e => saveQuestion(question)}>+</button>
    }

    return (
        <div className="container border rounded">
            <div className="row">
                <div className="col-9 border ">
                    <p></p>
                    <h5>Teema: {currentBlock.topic}</h5>
                    <p></p>
                    {currentBlock.topicQuestions!.map((question, i) => (
                        <div className="col">
                            <h5>&nbsp;</h5>
                            <h5 className="font-weight-bold">
                                Küsimus #{i + 1}&nbsp;&nbsp;&nbsp;
                                {saveQuestionButton(question)}
                                
                            </h5>
                            <p></p>
                            <p>{question.text}</p>
                            <p className="font-weight-bold">{question.question}({question.points}p)</p>
                            {question.questionAnswers?.map((answer, i) => (
                                <p className="font-weight-bold text-green">Õige vastus: {answer.answer}</p>
                            ))}
                        </div>

                    ))}
                </div>
                <div className="col-3 border">
                    <div className="col">

                    </div>
                    <div className="col">
                        <p className="text-center">_____</p>
                        <button type="button" className="btn button-bg-purple-dark btn-block" data-toggle="modal" data-target="#pointsModal">Punktid</button>
                        <PointsModal quiz={quiz}></PointsModal>
                        <p className="text-center">_____</p>

                        {quiz.quizTopics?.map((topic, i) => (
                            <button type="button" className="btn button-outline-purple-dark btn-block" onClick={e => selectTopic(topic)}>{topic.topic}</button>
                        ))}
                        <p></p>
                        {adminButtons()}
                    </div>
                </div>
            </div>
        </div>
    )

}

export default FinishedQuizView;
