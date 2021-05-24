import { TextField } from "@material-ui/core";
import React, { useContext, useEffect, useState } from "react";
import { BaseService } from "../../../services/BaseService";
import { AppContext } from "../../../context/AppContext";
import { IQuizTopicWithCounter } from "../../../domain/custom/IQuizTopicWithCounter";
import { IQuizDTO } from "../../../domain/IQuizDTO";
import { IQuizTopicDTO } from "../../../domain/IQuizTopicDTO";
import { useHistory, useParams } from "react-router-dom";
import { ITeamDTO } from "../../../domain/ITeamDTO";
import { ITopicQuestionDTO } from "../../../domain/ITopicQuestionDTO";
import { IQuestionAnswerDTO } from "../../../domain/IQuestionAnswerDTO";
import { ITeamAnswersCustomDTO } from "../../../domain/custom/TeamAnswersCustomDTO";
import $ from 'jquery';
import { ITeamAnswerDTO } from "../../../domain/ITeamAnswerDTO";
import PointsModal from "../../shared/PointsModal";


const QuizGame = () => {
    let { id } = useParams<{ id: string }>();
    const history = useHistory();
    const appContext = useContext(AppContext);
    const [quiz, setQuiz] = useState({ teams: [] as ITeamDTO[], quizTopics: [] as IQuizTopicDTO[] } as IQuizDTO);
    const [showAnswers, setShowAnwsers] = useState(false);
    const [counter, setCounter] = useState(0);
    const [blockNr, setBlockNr] = useState(0);
    const [currentBlock, setCurrentBlock] = useState({ topicQuestions: [] as ITopicQuestionDTO[] } as IQuizTopicDTO);
    const [insertAnswerBlock, setInsertAnswerBlock] = useState({ topicQuestions: [] as ITopicQuestionDTO[] } as IQuizTopicDTO);
    const [answers, setAnswers] = useState([] as ITeamAnswersCustomDTO[])
    const [selectedTeam, setSelectedTeam] = useState({ id: '' } as ITeamDTO)



    const data = async () => {
        await BaseService
            .getEntity<IQuizDTO>(id, 'quiz', appContext.data.token)
            .then(data => {
                if (data!.appUserId === appContext.data.appUserId) {
                    setQuiz(data!);
                    setCurrentBlock(data!.quizTopics![blockNr])
                    setSelectedTeam(data!.teams![0])
                    console.log(data)
                }
                else {
                    history.push('/upcomingquiz/' + data!.id)
                }
            });
    }
    const addAnswers = async () => {
        await BaseService
            .createEntity<ITeamAnswerDTO[]>(answers as ITeamAnswerDTO[], 'teamAnswers', appContext.data.token)
            .then(d => {
                data()
            });
    }

    const endGame = async () => {
        var entity = {id: quiz.id, appUserId: quiz.appUserId, title: quiz.title, start: quiz.start, finished: true} as IQuizDTO;
        await BaseService
            .updateEntity<IQuizDTO>(entity, 'quiz', appContext.data.token)
            .then(d => {
                history.push("/myQuizzes")
            });
    }

    useEffect(() => {
        data();
    }, []);

    useEffect(() => {
        const timer =
            counter > 0 && setInterval(() => setCounter(counter - 1), 1000);
        if (timer) {
            return () => clearInterval(timer);
        }
    }, [counter]);

    function nextBlock() {
        if (blockNr + 1 <= quiz.quizTopics!.length - 1) {
            setCurrentBlock(quiz.quizTopics![blockNr + 1]);
            setBlockNr(blockNr + 1);
            setCounter(0);
        }
        return
    }
    function endTopicButtons(){
        if (blockNr + 1 === quiz.quizTopics!.length) {
            return (
                <button type="button" className="btn btn-lg button-bg-purple-dark btn-block" onClick={e => endGame()}>Lõpeta mäng</button>
            )
        }
        return(
            <button type="button" className="btn btn-lg button-bg-purple-dark btn-block" onClick={e => nextBlock()}>Järgmine blokk</button>
        )
    }

    function testTimer() {
        setCounter(currentBlock.timeLimit);
    }
    function toggleAnswer() {
        setShowAnwsers(!showAnswers);
    }
    function answersHtml(answer: IQuestionAnswerDTO) {
        if (answer.correct && showAnswers) {
            return (
                <p className="font-weight-bold text-green">Õige vastus: {answer.answer}</p>
            )
        }

    }

    function timerRunning() {
        if (counter > 0) {
            return (
                <h4 className="d-inline text-center">Taimer: {counter}</h4>
            )
        }
        else {
            return (
                <h4 className="d-inline text-center">Taimer: {currentBlock.timeLimit}</h4>
            )
        }

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
                            <h5 className="font-weight-bold">Küsimus #{i + 1}</h5>
                            <p></p>
                            <p>{question.text}</p>
                            <p className="font-weight-bold">{question.question}({question.points}p)</p>
                            {question.questionAnswers?.map((answer, i) => (
                                <>{answersHtml(answer)}</>
                            ))}
                        </div>

                    ))}
                    <button type="button" className="btn button-bg-purple-dark btn-sm float-right" onClick={e => toggleAnswer()}>Näita vastuseid</button>
                </div>
                <div className="col-3 border">
                    <div className="col">
                        {timerRunning()}
                        <button type="button" className="btn button-bg-purple-dark d-block " onClick={e => testTimer()}>Start</button>
                    </div>
                    <div className="col">
                        <p className="text-center">_____</p>
                        <p className="text-center">Sisesta vastused:</p>
                        {quiz.quizTopics?.map((topic, i) => (
                            <>
                                <button type="button" className="btn button-outline-purple-dark btn-block" data-toggle="modal" data-target="#exampleModal" onClick={e => selectTopic(topic)}>{topic.topic}</button>
                                {insertAnswerModal(topic)}
                            </>
                        ))}

                    </div>
                    <div className="col">
                        <p className="text-center">_____</p>
                        <button type="button" className="btn button-bg-purple-dark btn-block" data-toggle="modal" data-target="#pointsModal">Punktid</button>
                        <PointsModal quiz={quiz}></PointsModal>
                        <p></p>
                        {endTopicButtons()}
                        <p></p>

                    </div>
                </div>
            </div>
        </div>
    );
    function selectTopic(topic: IQuizTopicDTO) {
        setInsertAnswerBlock(topic);
        var x = [] as ITeamAnswersCustomDTO[];
        topic.topicQuestions!.forEach(question => {
            x.push({ topicQuestionId: question.id, teamId: '', answer: '', correct: false, points: 0, topicQuestion: question } as ITeamAnswersCustomDTO)
        });
        setAnswers(x)
    }

    function handleQuizAnswers(e: React.ChangeEvent<HTMLTextAreaElement>, answerDTO: ITeamAnswersCustomDTO) {
        var { name, value } = e.target;
        var prev = answers;
        var i = prev.findIndex(a => a.topicQuestionId === answerDTO.topicQuestionId)
        var old = prev.find(a => a.topicQuestionId === answerDTO.topicQuestionId)
        prev[i] = {
            topicQuestionId: answerDTO.topicQuestionId, topicQuestion: answerDTO.topicQuestion, teamId: selectedTeam.id, team: selectedTeam,
            answer: value, correct: old!.correct, points: old!.points
        } as ITeamAnswersCustomDTO;
        setAnswers(prev);
        console.log(prev)
    }

    function handleCheckbox(e: React.ChangeEvent<HTMLInputElement>, answerDTO: ITeamAnswersCustomDTO) {
        var prev = answers;
        var i = prev.findIndex(a => a.topicQuestionId === answerDTO.topicQuestionId)
        var old = prev.find(a => a.topicQuestionId === answerDTO.topicQuestionId)
        prev[i] = {
            topicQuestionId: answerDTO.topicQuestionId, topicQuestion: answerDTO.topicQuestion, teamId: selectedTeam.id, team: selectedTeam,
            answer: old!.answer, correct: e.target.checked, points: old!.points
        } as ITeamAnswersCustomDTO;
        setAnswers(prev);
        console.log(prev)
    }

    function handlePoints(e: React.ChangeEvent<HTMLTextAreaElement | HTMLInputElement>, answerDTO: ITeamAnswersCustomDTO) {
        var { name, value } = e.target;
        var prev = answers;
        var i = prev.findIndex(a => a.topicQuestionId === answerDTO.topicQuestionId)
        var old = prev.find(a => a.topicQuestionId === answerDTO.topicQuestionId)
        prev[i] = {
            topicQuestionId: answerDTO.topicQuestionId, topicQuestion: answerDTO.topicQuestion, teamId: selectedTeam.id, team: selectedTeam,
            answer: old!.answer, correct: old!.correct, points: Number(value)
        } as ITeamAnswersCustomDTO;
        setAnswers(prev);
        console.log(prev)
    }

    function handleTeam(e: React.ChangeEvent<HTMLSelectElement>) {
        var team = quiz.teams?.find(a => a.id === e.target.value)
        setSelectedTeam(team!)
        console.log(selectedTeam)
    }

    function insertAnswerModal(topic: IQuizTopicDTO) {
        return (
            <div className="modal fade" id="exampleModal" tabIndex={-1} aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div className="modal-dialog modal-lg">
                    <div className="modal-content">
                        <div className="modal-header">
                            <h5 className="modal-title" id="exampleModalLabel">Sisesta tiimi vastused - {topic.topic}</h5>
                            <button type="button" className="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div className="modal-body">
                            <div className="form-group">
                                <h4 className="text-center">Vali tiim</h4>
                                <select className="form-control " id="exampleFormControlSelect1" onChange={e => handleTeam(e)}>
                                    {quiz.teams?.map((team, i) => (
                                        <option value={team.id}>{team.name}</option>
                                    ))}
                                </select>
                            </div>
                            <form>
                                {answers.map((answerDTO, i) => (
                                    <>
                                        <div className="font-weight-bold">Küsimus {i+1}# {answerDTO.topicQuestion.question}({answerDTO.topicQuestion.points}p)</div>
                                        <textarea className="form-control"
                                            id="standard-basic"
                                            name="answer"
                                            placeholder="vastus"
                                            onChange={e => handleQuizAnswers(e, answerDTO)} />
                                        <p></p>
                                        <div className="form-check">
                                            <input name="correct" type="checkbox" className="form-check-input" id="exampleCheck1" onChange={e => handleCheckbox(e, answerDTO)} />
                                            <label className="form-check-label" htmlFor="exampleCheck1">Õige vastus?</label>
                                        </div>
                                        <p></p>
                                    Points:&nbsp;&nbsp;&nbsp;
                                    <TextField
                                            id="standard-basic"
                                            name="points"
                                            placeholder="0"
                                            type="number"

                                            onChange={e => handlePoints(e, answerDTO)} />
                                        <p>&nbsp;</p>
                                    </>
                                ))}

                            </form>
                        </div>
                        <div className="modal-footer">
                            <button type="button" className="btn btn-secondary" data-dismiss="modal">Sulge</button>
                            <button type="button" className="btn button-bg-purple-dark" onClick={e => addAnswers()}>Salvesta vastused</button>
                        </div>

                    </div>
                </div>
            </div>
        )
    }

}

export default QuizGame;

