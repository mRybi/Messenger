import { FC, useCallback, useState } from 'react';
import { FieldValues, SubmitHandler, useForm } from 'react-hook-form';
import { BsGithub, BsGoogle } from 'react-icons/bs';
import { useAuthenticateMutation, useRegisterMutation } from '../../../services/auth/authApi';
import { AuthSocialButton, Button, Input } from '.';
import { useLocation, useNavigate } from 'react-router-dom';
import toast from 'react-hot-toast';

type Variant = "LOGIN" | "REGISTER";

export const AuthForm: FC = () => {
  const [variant, setVariant] = useState<Variant>("LOGIN");
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [registerUser] = useRegisterMutation();
  const [loginUser, result] = useAuthenticateMutation();

  const toggleVariant = useCallback(() => setVariant(variant === 'LOGIN' ? "REGISTER" : "LOGIN"), [variant]);

  const { register, handleSubmit, reset, formState: { errors } } = useForm<FieldValues>({
    defaultValues: {
      name: "",
      email: "",
      password: ""
    }
  });

  let navigate = useNavigate();
  let location = useLocation();
  let redirectBackTo = location.state?.from?.pathname || "/conversations";

  const onSubmit: SubmitHandler<FieldValues> = async ({ name, email, password }) => {
    setIsLoading(true);

    if (variant === "REGISTER") {
      await registerUser({ name, email, password }).unwrap().then(() => {
        toast.success('Successfully registered! Now you can login :)', {
          position: "bottom-center"
        });
        toggleVariant();
        setIsLoading(false);
      }).catch(() => {
        toast.error('This email is already taken. Try another one :)', {
          position: "bottom-center"
        });
        setIsLoading(false);
      })
    }

    if (variant === "LOGIN") {
      await loginUser({ email, password }).unwrap().then(() => {
        toast.success('Successfully logged!', {
          position: "bottom-center"
        });

        navigate(redirectBackTo, { replace: true });

      }).catch(() => {
        toast.error('Wrong password or email. Try again.', {
          position: "bottom-center"
        });
        setIsLoading(false);
      })
    }
  }

  return (
    <div className="mt-8 sm:mx-auto sm:w-full sm:max-w-md">
      <div
        className="
      bg-white
        px-4
        py-8
        shadow
        sm:rounded-lg
        sm:px-10
      "
      >
        <form
          className="space-y-6"
          onSubmit={handleSubmit(onSubmit)}
        >
          {variant === 'REGISTER' && (
            <Input
              disabled={isLoading}
              register={register}
              errors={errors}
              required
              id="name"
              label="Name"
            />
          )}
          <Input
            disabled={isLoading}
            register={register}
            errors={errors}
            required
            id="email"
            label="Email address"
            type="email"
          />
          <Input
            disabled={isLoading}
            register={register}
            errors={errors}
            required
            id="password"
            label="Password"
            type="password"
          />
          <div>
            <Button disabled={isLoading} fullWidth type="submit">
              {variant === 'LOGIN' ? 'Sign in' : 'Register'}
            </Button>
          </div>
        </form>

        <div className="mt-6">
          <div className="relative">
            <div
              className="
              absolute 
              inset-0 
              flex 
              items-center
            "
            >
              <div className="w-full border-t border-gray-300" />
            </div>
            <div className="relative flex justify-center text-sm">
              <span className="bg-white px-2 text-gray-500">
                Or continue with
              </span>
            </div>
          </div>

          <div className="mt-6 flex gap-2">
            <AuthSocialButton
              icon={BsGithub}
              onClick={() => console.log('github')}
            />
            <AuthSocialButton
              icon={BsGoogle}
              onClick={() => console.log('google')}
            />
          </div>
        </div>
        <div
          className="
          flex 
          gap-2 
          justify-center 
          text-sm 
          mt-6 
          px-2 
          text-gray-500
        "
        >
          <div>
            {variant === 'LOGIN' ? 'New to Messenger?' : 'Already have an account?'}
          </div>
          <div
            onClick={toggleVariant}
            className="underline cursor-pointer"
          >
            {variant === 'LOGIN' ? 'Create an account' : 'Login'}
          </div>
        </div>
      </div>
    </div>
  )
}




