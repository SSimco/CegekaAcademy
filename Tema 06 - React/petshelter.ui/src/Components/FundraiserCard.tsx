import { Alert, Button, Card, CardActions, CardContent, Dialog, DialogTitle, Stack, TextField, Typography } from "@mui/material";
import { Fundraiser } from "../Models/Fundraiser";
import { useState } from "react";
import { FundraiserDonationRequest } from "../Models/FundraiserDonationRequest";
import { Person } from "../Models/Person";
import { DatePicker } from "@mui/x-date-pickers";
import dayjs, { Dayjs } from "dayjs";
import { validatePersonId, validatePersonName } from "../Validators/PersonValidators";
import { validateDonationAmount } from "../Validators/FundraiserValidators";

export interface IFundraiserCardProps {
    fundraiser: Fundraiser;
    // returns an error message if the donation request failed
    handleDonate: (donationRequest: FundraiserDonationRequest) => Promise<string>;
}
interface IFundraiserStatusProps {
    status: string;
    dueDate: Date;
}

const FundraiserStatus = (props: IFundraiserStatusProps) => {
    if (props.status === "Active") {
        return (
            <Stack
                direction="row"
                justifyContent="space-between"
                alignItems="center"
            >
                <Typography variant="h6" color="text.primary">
                    {props.status}
                </Typography>
                <Typography variant="h6" color="text.primary">
                    Due {props.dueDate.toString().split("T")[0]}
                </Typography>
            </Stack>
        )
    }
    else {
        return (
            <Typography variant="h6" color="text.primary">
                {props.status}
            </Typography>
        );
    }
}

export const FundraiserCard = (props: IFundraiserCardProps) => {
    const [donationError, setDonationError] = useState("");
    const [open, setOpen] = useState(false);
    const [openConfirmDialog, setOpenConfirmDialog] = useState(false);
    const [donorName, setDonorName] = useState("");
    const [donorId, setDonorId] = useState("");
    const [donorDateOfBirth, setDonorDateOfBirth] = useState<Dayjs | null>(dayjs());
    const [donationAmount, setDonationAmount] = useState("");
    const [nameError, setNameError] = useState("");
    const [idError, setIdError] = useState("");
    const [donationAmountError, setDonationAmountError] = useState("");

    const handleDonateDialogClick = () => {
        setOpen(true);
    };

    const handleDonateDialogClose = () => {
        setDonorName("");
        setDonorId("");
        setDonorDateOfBirth(dayjs());
        setDonationAmount("");
        setNameError("");
        setIdError("");
        setDonationAmountError("");
        setDonationError("");
        setOpen(false);
    };

    const isRequestValid = () => {
        var isValid = true;
        const nameValidationMessage = validatePersonName(donorName);
        if (nameValidationMessage !== null) {
            isValid = false;
            setNameError(nameValidationMessage);
        }
        const idValidationMessage = validatePersonId(donorId);
        if (idValidationMessage !== null) {
            isValid = false;
            setIdError(idValidationMessage);
        }
        const donationValidationMessage = validateDonationAmount(donationAmount);
        if (donationValidationMessage !== null) {
            isValid = false;
            setDonationAmountError(donationValidationMessage);
        }
        return isValid;
    };

    const handleDonateClick = async () => {
        if (!isRequestValid())
            return;
        const donationRequest = new FundraiserDonationRequest(props.fundraiser.id,
            Number(donationAmount),
            new Person(donorName, Number(donorId), donorDateOfBirth?.toDate()));
        const donationError = await props.handleDonate(donationRequest);
        if (donationError === "") {
            props.fundraiser.currentRaisedAmount += Number(donationAmount);
            if (props.fundraiser.currentRaisedAmount >= props.fundraiser.goalValue || props.fundraiser.dueDate >= new Date()) {
                props.fundraiser.status = "Closed";
            }
            setOpen(false);
            setOpenConfirmDialog(true);
        } else {
            console.log(donationError);
            setDonationError(donationError);
        }
    };

    return (
        <Card sx={{ maxWidth: 350 }}>
            <CardContent>
                <Typography variant="h5" component="div">{props.fundraiser.name}</Typography>
                <FundraiserStatus status={props.fundraiser.status} dueDate={props.fundraiser.dueDate} />
                <Typography variant="h6" color="text.primary">
                    ${props.fundraiser.currentRaisedAmount} raised of ${props.fundraiser.goalValue}
                </Typography>
            </CardContent>
            <CardActions>
                <Button variant="contained" onClick={handleDonateDialogClick}>
                    Donate
                </Button>

                <Dialog open={open} onClose={handleDonateDialogClose} >
                    <DialogTitle>Donate to {props.fundraiser.name}</DialogTitle>
                    <Stack
                        component="form"
                        justifyContent="space-evenly"
                        spacing={5}
                        noValidate
                        autoComplete="off"
                        margin={5}
                    >
                        {donationError !== "" && <Alert severity="error">{donationError} </Alert>}
                        <TextField
                            label="Full name"
                            variant="outlined"
                            onChange={() => {
                                if (nameError !== "") {
                                    setNameError("");
                                }
                            }}
                            onBlur={event => {
                                setDonorName(event.target.value)
                            }}
                            error={nameError !== ""}
                            helperText={nameError}
                        />
                        <TextField
                            label="ID number"
                            variant="outlined"
                            onChange={() => {
                                if (idError !== "") {
                                    setIdError("");
                                }
                            }}
                            onBlur={event => {
                                setDonorId(event.target.value);
                            }}
                            error={idError !== ""}
                            helperText={idError}
                        />
                        <DatePicker label="Birthdate" value={donorDateOfBirth} onChange={(date) => setDonorDateOfBirth(date)} />
                        <TextField
                            label="Donation amount"
                            variant="outlined"
                            onChange={() => {
                                if (donationAmountError !== "") {
                                    setDonationAmountError("");
                                }
                            }}
                            onBlur={event => {
                                setDonationAmount(event.target.value);
                            }}
                            error={donationAmountError !== ""}
                            helperText={donationAmountError}
                        />

                        <Button variant="contained" onClick={handleDonateClick}>Donate</Button>
                    </Stack>
                </Dialog>
            </CardActions>
            <Dialog open={openConfirmDialog} onClose={() => setOpenConfirmDialog(false)} >
                <DialogTitle>Donation successful!</DialogTitle>
                <Stack
                    alignItems="center"
                    spacing={2}
                    margin={5}
                >
                    <Typography variant="h6" color="text.primary">
                        Thank you for donating ${donationAmount} to {props.fundraiser.name}
                    </Typography>
                    <Button variant="contained" onClick={() => setOpenConfirmDialog(false)}>OK</Button>
                </Stack>
            </Dialog>
        </Card>
    );
}
