import React, { useMemo } from 'react';
import './Register.scss';
import * as yup from 'yup';
import { passwordRegex, onlyLetters } from '../../constants/Regexes';
import { Controller, useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup/dist/yup';
import { useNavigate } from 'react-router-dom';
import { Button, TextField, Typography } from '@mui/material';
import { register } from '../../api/controllers/AuthClient';
import AuthContext from '../../contexts/AuthContext';
import { useSnackbar } from 'notistack';
import axios from 'axios';

const validationSchema = yup.object({
  firstName: yup
    .string()
    .matches(onlyLetters, 'First name can only contain Latin letters.')
    .required('First name is required!'),
  lastName: yup
    .string()
    .matches(onlyLetters, 'Last name can only contain Latin letters.')
    .required('Last name is required!'),
  email: yup.string().email('Incorrect email!').required('Email is required!'),
  password: yup
    .string()
    .required('Password is required')
    .min(8, 'Password must be longer than 8 characters!')
    .matches(passwordRegex, 'Password must contain uppercase letter, digit and special character!'),
  confirmPassword: yup
    .string()
    .oneOf([yup.ref('password')], 'Passwords do not match!')
    .required('Password confirmation is required!'),
});

interface IFormData {
  firstName: string;
  lastName: string;
  email: string;
  password: string;
  confirmPassword: string;
  addressLine1: string;
  addressLine2: string;
  addressCity: string;
  addressPostalCode: string;
}

const Register = () => {
  const { enqueueSnackbar } = useSnackbar();
  const { signIn } = React.useContext(AuthContext);

  const defaultValue = useMemo<IFormData>(
    () => ({
      firstName: '',
      lastName: '',
      email: '',
      password: '',
      confirmPassword: '',
      addressLine1: '',
      addressLine2: '',
      addressCity: '',
      addressPostalCode: '',
    }),
    [],
  );

  const {
    control,
    handleSubmit,
    formState: { errors },
  } = useForm<IFormData>({
    defaultValues: useMemo(() => {
      return defaultValue;
    }, [defaultValue]),
    resolver: yupResolver(validationSchema),
  });

  const navigate = useNavigate();

  const onSubmit = async (e: IFormData) => {
    try {
      const requestDto = {
        firstName: e.firstName,
        lastName: e.lastName,
        email: e.email,
        password: e.password,
        address: { line1: e.addressLine1, line2: e.addressLine2, city: e.addressCity, postalCode: e.addressPostalCode },
      };

      await register(requestDto).then(async (response) => {
        if (response.errors.length > 0) {
          throw new Error(response.errors[0]);
        }
        enqueueSnackbar('Account successfully created', { variant: 'success' });
        await signIn(e.email, e.password);
        navigate('/drops');
      });
    } catch (e) {
      const message = axios.isAxiosError(e) ? e.message : `${e}`;
      enqueueSnackbar(message, { variant: 'error' });
    }
  };

  return (
    <div className="register-container">
      <Typography variant="h3">Create Account</Typography>

      <form>
        <div>
          <Controller
            name={'firstName'}
            control={control}
            render={({ field: { onChange, value } }) => (
              <TextField
                className="register-item"
                onChange={onChange}
                value={value}
                label={'First name'}
                helperText={errors.firstName?.message}
                error={!!errors.firstName}
              />
            )}
          />
        </div>

        <div>
          <Controller
            name={'lastName'}
            control={control}
            render={({ field: { onChange, value } }) => (
              <TextField
                className="register-item"
                onChange={onChange}
                value={value}
                label={'Last name'}
                helperText={errors.lastName?.message}
                error={!!errors.lastName}
              />
            )}
          />
        </div>

        <div>
          <Controller
            name={'email'}
            control={control}
            render={({ field: { onChange, value } }) => (
              <TextField
                className="register-item"
                type="email"
                onChange={onChange}
                value={value}
                label={'Email'}
                helperText={errors.email?.message}
                error={!!errors.email}
              />
            )}
          />
        </div>

        <div>
          <Controller
            name={'password'}
            control={control}
            render={({ field: { onChange, value } }) => (
              <TextField
                className="register-item"
                type="password"
                onChange={onChange}
                value={value}
                label={'Password'}
                helperText={errors.password?.message}
                error={!!errors.password}
              />
            )}
          />
        </div>

        <div>
          <Controller
            name={'confirmPassword'}
            control={control}
            render={({ field: { onChange, value } }) => (
              <TextField
                className="register-item"
                type="password"
                onChange={onChange}
                value={value}
                label={'Confirm password'}
                helperText={errors.confirmPassword?.message}
                error={!!errors.confirmPassword}
              />
            )}
          />
        </div>

        <div>
          <Controller
            name={'addressLine1'}
            control={control}
            render={({ field: { onChange, value } }) => (
              <TextField className="register-item" onChange={onChange} value={value} label={'Line 1'} />
            )}
          />
        </div>

        <div>
          <Controller
            name={'addressLine2'}
            control={control}
            render={({ field: { onChange, value } }) => (
              <TextField className="register-item" onChange={onChange} value={value} label={'Line 2'} />
            )}
          />
        </div>

        <div>
          <Controller
            name={'addressPostalCode'}
            control={control}
            render={({ field: { onChange, value } }) => (
              <TextField className="register-item" onChange={onChange} value={value} label={'Postal code'} />
            )}
          />
        </div>

        <div>
          <Controller
            name={'addressCity'}
            control={control}
            render={({ field: { onChange, value } }) => (
              <TextField className="register-item" onChange={onChange} value={value} label={'City'} />
            )}
          />
        </div>

        <Button className="register-item" sx={{ width: '100%' }} type="submit" onClick={handleSubmit(onSubmit)}>
          Submit
        </Button>
      </form>
    </div>
  );
};

export default Register;
