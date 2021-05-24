import { TextField } from "@material-ui/core";
import React, { useContext, useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { BaseService } from "../../../services/BaseService";
import { AppContext } from "../../../context/AppContext";
import { IQuestionAnswerDTO } from "../../../domain/IQuestionAnswerDTO";
import { IQuizDTO } from "../../../domain/IQuizDTO";
import { IQuizTopicDTO } from "../../../domain/IQuizTopicDTO";
import { ITopicQuestionDTO } from "../../../domain/ITopicQuestionDTO";
import { QuestionType } from "../../../base/QuestionType";



const EditQuiz = () => {
    let { id } = useParams<{ id: string }>();
    const appContext = useContext(AppContext);
    const [quizData, setQuizData] = useState({ title: '', finished: false, start: new Date(), appUserId: appContext.data.appUserId, quizTopics: [] as IQuizTopicDTO[] } as IQuizDTO);
    const [selectedTopic, setSelectedTopic] = useState({ quizId: '', topic: '', timeLimit: 0, topicQuestions: [] as ITopicQuestionDTO[] } as IQuizTopicDTO);
    const [selectedQuestions, setSelectedQuestions] = useState([] as ITopicQuestionDTO[]);
    const [selectedQuestion, setSelectedQuestion] = useState({ question: '', text: '', points: 0, type: QuestionType.NORMAL, quizTopicId: '' } as ITopicQuestionDTO);
    const [selectedAnswers, setSelectedAnswers] = useState([] as IQuestionAnswerDTO[]);
    const [selectedAnswer, setSelectedAnswer] = useState({ answer: '', correct: false, topicQuestionId: '' } as IQuestionAnswerDTO);


    const data = async () => {
        await BaseService
            .getSingle<IQuizDTO>('quiz/' + id)
            .then(data => {
                setQuizData(data!);
                console.log(data)
            });
    }

    useEffect(() => {
        data();
        console.log(quizData)
    }, []);


    function selectTopic(topic: IQuizTopicDTO) {
        setSelectedTopic(topic);
        if (topic.topicQuestions) {
            console.log(topic)
            setSelectedQuestions(topic.topicQuestions)
            setSelectedAnswers([]);
        }

    };

    function editSelectedTopic(e: React.ChangeEvent<HTMLInputElement>) {
        var { name, value } = e.target;
        if (isNaN(+value)) {
            setSelectedTopic({ ...selectedTopic, [name]: value });
        } else {
            setSelectedTopic({ ...selectedTopic, [name]: Number(value) });
        }
    };

    const editTopic = async () => {
        var topicData = {id: selectedTopic.id, topic: selectedTopic.topic, timeLimit: selectedTopic.timeLimit, quizId: selectedTopic.quizId } as IQuizTopicDTO
        await BaseService.updateEntity<IQuizTopicDTO>(topicData, 'quizTopics', appContext.data!.token!)
            .then(e => {
                setSelectedTopic({...selectedTopic, id: ''})
                data();
            })
    };

    const createTopic = async () => {
        var topicData = { topic: "New topic", timeLimit: 0, quizId: quizData.id } as IQuizTopicDTO
        await BaseService.createEntity<IQuizTopicDTO>(topicData, 'quizTopics', appContext.data!.token!)
            .then(e => {
                setSelectedTopic(e!)
                data();
            })
    };

    function selectQuestion(question: ITopicQuestionDTO) {

        setSelectedQuestion(question);
        console.log(question)
        if (question.questionAnswers) {
            setSelectedAnswers(question.questionAnswers)
            setSelectedAnswer({ answer: '', correct: false, topicQuestionId: '' } as IQuestionAnswerDTO )
        }
    };

    function editSelectedQuestion(e: React.ChangeEvent<HTMLInputElement> | React.ChangeEvent<HTMLTextAreaElement>) {
        var { name, value } = e.target;
        if (isNaN(+value)) {
            setSelectedQuestion({ ...selectedQuestion, [name]: value });
        } else {
            setSelectedQuestion({ ...selectedQuestion, [name]: Number(value) });
        }
    }


    const editQuestion = async () => {
        var questionData = {
            id: selectedQuestion.id, quizTopicId: selectedQuestion.quizTopicId,
            question: selectedQuestion.question, text: selectedQuestion.text, points: selectedQuestion.points, type: QuestionType.NORMAL
        } as ITopicQuestionDTO;

        var newQuestions = selectedQuestions;
        await BaseService.updateEntity<ITopicQuestionDTO>(questionData, 'topicQuestions')
            .then(e => {
                const index = newQuestions.findIndex(q => q.id === questionData.id);
                questionData.questionAnswers = selectedAnswers;
                newQuestions[index] = questionData;
                setSelectedQuestions(newQuestions);
                BaseService.createEntity<IQuestionAnswerDTO[]>(selectedAnswers, 'questionAnswers/addList')
                    .then(e => {
                    })
            })
        setSelectedQuestion({ ...selectedQuestion, id: '' })
        

    };


    const createQuestion = async () => {
        var topicData = { id: selectedTopic.id, topic: selectedTopic.topic, timeLimit: selectedTopic.timeLimit, quizId: selectedTopic.quizId } as IQuizTopicDTO
        await BaseService.updateEntity<IQuizTopicDTO>(topicData, 'quizTopics', appContext.data!.token!)
            .then(e => { })

        var questionData = { quizTopicId: selectedTopic.id, question: "What is love?", text: "Informative text about question", points: 0, type: QuestionType.NORMAL } as ITopicQuestionDTO
        await BaseService.createEntity<ITopicQuestionDTO>(questionData, 'topicQuestions', appContext.data!.token!)
            .then(d => {
                data()
                setSelectedQuestions([...selectedQuestions, d!])
                setSelectedAnswers([]);
            });
    };

    function editAnswer(e: React.ChangeEvent<HTMLInputElement>, checkBox: boolean) {
        var { name, value } = e.target;
        if (checkBox) {
            setSelectedAnswer({ ...selectedAnswer, [name]: e.target.checked , topicQuestionId: selectedQuestion.id});
        } else {
            setSelectedAnswer({ ...selectedAnswer, [name]: value , topicQuestionId: selectedQuestion.id});
        }
    }

    function addAnswer() {
        setSelectedAnswers([...selectedAnswers, selectedAnswer])
        console.log(selectedAnswers)
    }
    function correctAnswer(correct: boolean) {
        if (correct) {
            return <>Õige vastus</>
        }
        return <>Vale vastus</>
    }

    function checkSelectedQuestion(question: ITopicQuestionDTO) {
        if (selectedQuestion.id === question.id) {
            return (
                <>
                    <p></p>
                    <div className="container border rounded">
                        <p></p>
                        <form className="form-group">
                            <div className="mb-3">
                                <label className="form-label">Tekst:</label>
                                <textarea className="form-control" id="exampleFormControlTextarea1" rows={4} value={selectedQuestion.text} name="text" onChange={e => editSelectedQuestion(e)}></textarea>
                            </div>
                            <div className="mb-3">
                                <label className="form-label">Küsimus</label>
                                <input value={selectedQuestion.question} type="text" name="question" className="form-control" id="formTopic" onChange={e => editSelectedQuestion(e)} />
                            </div>
                            <div className="mb-3">
                                <label className="form-label">Maksimum punktid:</label>
                                <input value={selectedQuestion.points} type="number" name="points" className="form-control" id="formTimeLimit" onChange={e => editSelectedQuestion(e)} />
                            </div>
                            <div className="mb-3">
                                <label className="form-label">Vastused:</label>
                            </div>
                            {selectedAnswers.map((answer, i) => (
                                <p>{answer.answer}({correctAnswer(answer.correct)})</p>
                            ))}
                            <p></p>
                            <input value={selectedAnswer.answer} type="text" name="answer" className="form-control" id="formTopic" onChange={e => editAnswer(e, false)} />
                            <div className="form-check">
                                <input name="correct" type="checkbox" className="form-check-input" id="exampleCheck1" onChange={e => editAnswer(e, true)} />
                                <label className="form-check-label" htmlFor="exampleCheck1">Õige vastus?</label>
                            </div>
                            <button type="button" className="btn button-bg-cyan d-block" onClick={() => addAnswer()}>Lisa uus vastus</button>
                            <p></p>
                            <button type="button" className="btn button-bg-purple-dark" onClick={editQuestion}>Salvesta küsimus</button>
                            <p></p>
                        </form>
                    </div>
                    <p></p>
                </>
            )
        }
        return <><p></p></>
    }

    function checkSelectedTopic() {
        if (!selectedTopic.id) {
            return <></>
        }
        else {
            return (
                <div className="container border border-3 rounded">
                    <p></p>
                    <form className="form-group">
                        <div className="mb-3">
                            <label className="form-label">Teema nimi</label>
                            <input value={selectedTopic.topic} type="text" name="topic" className="form-control" id="formTopic" onChange={e => editSelectedTopic(e)} />
                        </div>
                        <div className="mb-3">
                            <label className="form-label">Ajalimiit sekundites:</label>
                            <input value={selectedTopic.timeLimit} type="number" name="timeLimit" className="form-control" id="formTimeLimit" onChange={e => editSelectedTopic(e)} />
                        </div>
                        <div>
                            <h4>Küsimused:</h4>
                        </div>
                        {selectedQuestions.map((question, i) => (
                            <>
                                <div className="container border rounded">
                                    <h5>Küsimus #{i + 1}</h5>
                                    <div>{question.text}</div>
                                    <h6 className="fw-bold">{question.question}({question.points}p)</h6>
                                    <h6 className="fw-bold">Vastused:</h6>
                                    {question.questionAnswers?.map((answer, index) => (
                                        <div className="container">{answer.answer}: ({correctAnswer(answer.correct)})</div>
                                    ))}
                                    <p></p>
                                    <div className="modal-footer">
                                        <button type="button" className="btn button-bg-purple-dark" onClick={e => selectQuestion(question)}>Muuda</button>
                                        {checkSelectedQuestion(question)}
                                    </div>


                                </div>
                                <p></p>
                            </>
                        ))}
                        <button type="button" className="btn button-bg-purple-dark" onClick={createQuestion}>Lisa uus küsimus</button>
                        <div className="modal-footer">
                            
                            <button type="button" className="btn button-bg-cyan" onClick={editTopic}>Salvesta plokk</button>
                        </div>
                    </form>
                    <p></p>
                </div>
            )
        }
    };

    function navTopicButton(topic: IQuizTopicDTO){
        if (topic.id === selectedTopic.id) {
            return(<button type="button" className="btn button-bg-purple-dark" onClick={e => selectTopic(topic)}>{shortTopic(topic)}</button>)
        }
        return(<button type="button" className="btn button-outline-purple-dark" onClick={e => selectTopic(topic)}>{shortTopic(topic)}</button>)
    }

    return (
        <div className="container">
            <h3>Redigeeri {quizData.title}</h3>
            <div className="btn-group" role="group" aria-label="Basic outlined example">
                {quizData.quizTopics!.map((topic, i) => (
                    <>{navTopicButton(topic)}</>
                ))}
                <button type="button" className="btn button-bg-cyan" onClick={createTopic}>Lisa uus plokk</button>
            </div>
            <p></p>
            {checkSelectedTopic()}

        </div>

    );

    function shortTopic(topic: IQuizTopicDTO) {
        if (topic.topic.length > 16) {
            return (topic.topic.slice(0, 14)+ "...")
        }
        return topic.topic
    }
    


}

export default EditQuiz;
